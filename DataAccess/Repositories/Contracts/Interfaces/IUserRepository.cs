using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> RegisterUserAsync(string login, string password, string email);
    Task<UserEntity> AuthenticateUserAsync(string login, string password);
    Task<UserEntity> GetUserByIdAsync(int userId);
    Task UpdateUserAsync(int userId, string newEmail, string newPassword);
    Task DeleteUserAsync(int userId);
    Task<UserEntity> GetUserByLoginAsync(string login);
    Task UpdateUserRoleAsync(string login, int newRoleId);
}