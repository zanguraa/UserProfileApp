using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using UserProfile.Application.Features.UserProfiles.Commands;
using UserProfile.Application.Features.UserProfiles.Validators;
using UserProfile.WebApi.Endpoints;
using UserProfile.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// ---------------------- Services ----------------------

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Swagger (optional but helpful for testing)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserProfileValidator).Assembly);

// HttpContext accessor
builder.Services.AddHttpContextAccessor();

// Controllers (optional — if you use classic controllers too)
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

// Infrastructure-layer services registration (optional for later)
// builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
// builder.Services.AddScoped<IImageStorageService, LocalFileImageStorage>();

// Static file support — for serving profile pictures if using wwwroot/images
builder.Services.AddDirectoryBrowser(); // optional for debugging

// ---------------------- Build App ----------------------

var app = builder.Build();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Static files (for images or other assets)
app.UseStaticFiles(); // enables wwwroot/
                      // app.UseDirectoryBrowser(); // optional

app.UseHttpsRedirection();

// Optional classic controller support
app.MapControllers();

// Load endpoints from extension method
app.MapUserProfileEndpoints(); // our Minimal APIs in a clean way

app.Run();
