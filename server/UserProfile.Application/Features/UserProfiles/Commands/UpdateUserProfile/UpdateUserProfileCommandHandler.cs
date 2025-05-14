using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.Interfaces;
using UserProfile.Domain.Entities;

namespace UserProfile.Application.Features.UserProfiles.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, bool>
    {
        private readonly IUserProfileRepository _repository;

        public UpdateUserProfileCommandHandler(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            byte[]? image = null;
            if (request.ProfilePicture != null)
            {
                using var ms = new MemoryStream();
                await request.ProfilePicture.CopyToAsync(ms);
                image = ms.ToArray();
            }

            return await _repository.UpdateAsync(new UserProfileEntity
            {
                Id = request.Id,
                FullName = request.FullName,
                Age = request.Age,
                Nationality = request.Nationality,
                FavoriteCountries = request.FavoriteCountries,
                VisitedCountries = request.VisitedCountries,
                FavoriteFootballTeam = request.FavoriteFootballTeam,
                Hobbies = request.Hobbies,
                Bio = request.Bio,
                ProfilePicture = image
            });
        }
    }

}
