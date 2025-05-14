using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserProfile.Application.Interfaces;
using UserProfile.Infrastructure.Persistence;

namespace UserProfile.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<IUserRepository, UserRepository>();

        // სურათების byte[]-ზე შენახვის შემთხვევაში შეიძლება დაგვჭირდება სხვაც
        return services;
    }
}
