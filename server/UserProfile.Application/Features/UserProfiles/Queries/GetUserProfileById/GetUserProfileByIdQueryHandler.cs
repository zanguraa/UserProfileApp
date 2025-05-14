using MediatR;
using UserProfile.Application.Interfaces;
using UserProfile.Domain.Entities;

public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfileEntity?>
{
    private readonly IUserProfileRepository _repository;

    public GetUserProfileByIdQueryHandler(IUserProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserProfileEntity?> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
