namespace UserProfile.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string HashedPassword { get; set; } = default!;
    public string Role { get; set; } = "User"; 
}
