using MediatR;
using UserProfile.Application.Interfaces;
using UserProfile.Domain.Entities;

namespace UserProfile.Application.Features.UserProfiles.Queries;

public class GetUserProfilesQueryHandler : IRequestHandler<GetUserProfilesQuery, List<UserProfileEntity>>
{
    private readonly IUserProfileRepository _repository;

    public GetUserProfilesQueryHandler(IUserProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserProfileEntity>> Handle(GetUserProfilesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
