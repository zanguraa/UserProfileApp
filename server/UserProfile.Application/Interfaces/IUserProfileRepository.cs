
using UserProfile.Domain.Entities;

namespace UserProfile.Application.Interfaces;
public interface IUserProfileRepository
{
    Task<int> CreateAsync(UserProfileEntity entity);
    Task<UserProfileEntity?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(UserProfileEntity entity);
    Task<List<UserProfileEntity>> GetAllAsync();


    // შემდგომ დამატება: GetAllAsync, GetByIdAsync, DeleteAsync და ა.შ.
}
