namespace UserProfile.Infrastructure.Persistence.Models;

public class UserProfileRecord
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public int Age { get; set; }
    public string Nationality { get; set; } = default!;
    public string FavoriteCountries { get; set; } = default!; 
    public string VisitedCountries { get; set; } = default!;
    public string FavoriteFootballTeam { get; set; } = default!;
    public string Hobbies { get; set; } = default!;
    public string Bio { get; set; } = default!;
    public byte[]? ProfilePicture { get; set; }
    public DateTime CreatedAt { get; set; }
}
