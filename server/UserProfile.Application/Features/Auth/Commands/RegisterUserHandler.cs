using MediatR;
using UserProfile.Application.Interfaces;
using UserProfile.Domain.Entities;
using System.Security.Cryptography;
using BCrypt.Net;
using UserProfile.Application.Common.Exceptions;

namespace UserProfile.Application.Features.Auth.Commands;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, int>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsEmailTakenAsync(request.Email))
            throw new ConflictException("Email is already registered.");

        var user = new User
        {
            Email = request.Email,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        return await _userRepository.RegisterAsync(user);
    }
}
