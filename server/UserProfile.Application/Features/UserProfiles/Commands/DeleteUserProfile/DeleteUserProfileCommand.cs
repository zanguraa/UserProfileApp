using MediatR;

namespace UserProfile.Application.Features.UserProfiles.Commands.DeleteUserProfile;

public class DeleteUserProfileCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteUserProfileCommand(int id)
    {
        Id = id;
    }
}