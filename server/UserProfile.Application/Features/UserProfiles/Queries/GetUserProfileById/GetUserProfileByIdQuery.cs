using MediatR;
using UserProfile.Domain.Entities;

public class GetUserProfileByIdQuery : IRequest<UserProfileEntity?>
{
    public int Id { get; set; }

    public GetUserProfileByIdQuery(int id)
    {
        Id = id;
    }
}