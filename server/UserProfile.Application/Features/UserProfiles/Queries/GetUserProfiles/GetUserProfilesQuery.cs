using MediatR;
using UserProfile.Domain.Entities;

namespace UserProfile.Application.Features.UserProfiles.Queries;

public class GetUserProfilesQuery : IRequest<List<UserProfileEntity>>
{
}