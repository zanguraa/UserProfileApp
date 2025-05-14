using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.Interfaces;

namespace UserProfile.Application.Features.UserProfiles.Commands.DeleteUserProfile;

public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, bool>
{
    private readonly IUserProfileRepository _repository;

    public DeleteUserProfileCommandHandler(IUserProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}
