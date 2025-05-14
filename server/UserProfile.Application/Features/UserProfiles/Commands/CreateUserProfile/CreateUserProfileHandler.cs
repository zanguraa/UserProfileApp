using MediatR;
using UserProfile.Application.Interfaces;
using UserProfile.Domain.Entities;

namespace UserProfile.Application.Features.UserProfiles.Commands.CreateUserProfile
{
    public class CreateUserProfileHandler : IRequestHandler<CreateUserProfileCommand, int>
    {
        private readonly IUserProfileRepository _repository;

        public CreateUserProfileHandler(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            byte[] imageBytes = null;
            if (request.ProfilePicture != null)
            {
                using var ms = new MemoryStream();
                await request.ProfilePicture.CopyToAsync(ms);
                imageBytes = ms.ToArray();
            }

            var userProfile = new UserProfileEntity 
            {
                FullName = request.FullName,
                Age = request.Age,
                Nationality = request.Nationality,
                FavoriteCountries = request.FavoriteCountries,
                VisitedCountries = request.VisitedCountries,
                FavoriteFootballTeam = request.FavoriteFootballTeam,
                Hobbies = request.Hobbies,
                Bio = request.Bio,
                ProfilePicture = imageBytes,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.CreateAsync(userProfile);
        }
    }
}
