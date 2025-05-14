using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UserProfile.Application.Features.Auth.Commands;
using UserProfile.Application.Common.Exceptions;

namespace UserProfile.WebApi.Endpoints;

public class AuthEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (
            RegisterUserCommand command,
            ISender sender) =>
        {
            try
            {
                var id = await sender.Send(command);
                return Results.Ok(new { id });
            }
            catch (ConflictException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.Problem("Unexpected error: " + ex.Message);
            }
        });
    }
}
