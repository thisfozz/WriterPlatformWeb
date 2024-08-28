using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> RegisterUserAsync(string login, string password, string email, int roleId);
    Task<UserEntity> AuthenticateUserAsync(string login, string password);
    Task<UserEntity> GetUserByIdAsync(int userId);
    Task<UserEntity> GetUserByLoginAsync(string login);
    Task<bool> UpdateUserAsync(int userId, string newEmail, string newPassword);
    Task<bool> UpdateUserRoleAsync(string login, int newRoleId);
    Task<bool> DeleteUserAsync(int userId);
}