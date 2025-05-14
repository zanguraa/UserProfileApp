using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProfile.Application.Features.UserProfiles.Queries;
using UserProfile.Application.Features.UserProfiles.Commands.CreateUserProfile;
using UserProfile.Application.Features.UserProfiles.Commands.UpdateUserProfile;
using UserProfile.Application.Features.UserProfiles.Commands.DeleteUserProfile;

namespace UserProfile.WebApi.Endpoints;

public static class UserProfileEndpoints
{
    public static IEndpointRouteBuilder MapUserProfileEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/userprofiles", async (
     HttpContext context,
     [FromForm] CreateUserProfileCommand command,
     IValidator<CreateUserProfileCommand> validator,
     ISender sender) =>
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var id = await sender.Send(command);
            return Results.Ok(new { id });
        }).DisableAntiforgery(); 

        // GET — ყველა პროფილის წამოღება
        endpoints.MapGet("/api/userprofiles", async (
            ISender sender) =>
        {
            var result = await sender.Send(new GetUserProfilesQuery());
            return Results.Ok(result);
        });

        // GET by id
        endpoints.MapGet("/api/userprofiles/{id:int}", async (int id, ISender sender) =>
        {
            var profile = await sender.Send(new GetUserProfileByIdQuery(id));
            return profile is not null ? Results.Ok(profile) : Results.NotFound();
        });

        // DELETE
        endpoints.MapDelete("/api/userprofiles/{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteUserProfileCommand(id));
            return result ? Results.Ok() : Results.NotFound();
        });

        // PUT – Update (multipart/form-data)
        endpoints.MapPut("/api/userprofiles", async (
            [FromForm] UpdateUserProfileCommand command,
            ISender sender) =>
        {
            var result = await sender.Send(command);
            return result ? Results.Ok() : Results.NotFound();
        });


        return endpoints;
    }
}
