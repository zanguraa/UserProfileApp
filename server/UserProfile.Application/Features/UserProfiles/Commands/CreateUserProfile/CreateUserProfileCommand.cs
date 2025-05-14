using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfile.Application.Features.UserProfiles.Commands.CreateUserProfile;

public class CreateUserProfileCommand : IRequest<int>
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Nationality { get; set; }
    public List<string> FavoriteCountries { get; set; }
    public List<string> VisitedCountries { get; set; }
    public string FavoriteFootballTeam { get; set; }
    public List<string> Hobbies { get; set; }
    public string Bio { get; set; }
    public IFormFile ProfilePicture { get; set; }
}
