using MediatR;
using Microsoft.AspNetCore.Http;

namespace UserProfile.Application.Features.Auth.Commands;

public class RegisterUserCommand : IRequest<int>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}