using UserProfile.Domain.Entities;

namespace UserProfile.Application.Interfaces;

public interface IUserRepository
{
    Task<bool> IsEmailTakenAsync(string email);
    Task<int> RegisterAsync(User user);
    Task<User?> GetByEmailAsync(string email);
}