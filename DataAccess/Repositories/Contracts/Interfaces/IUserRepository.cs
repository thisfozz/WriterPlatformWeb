using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> RegisterUserAsync(UserEntity user); // РЕГИСТРАЦИЯ ПОЛЬЗОВАТЕЛЯ
    Task<bool> IsUserRegisteredByLoginAsync(string login);

    Task<UserEntity?> GetUserByIdAsync(Guid userId);
    Task<UserEntity?> GetUserByLoginOrEmailAsync(string loginOrEmail);
    Task<bool> UpdateEmailAsync(Guid userId, string newEmail);
    Task<bool> UpdatePasswordAsync(Guid userId, string password);
    Task<bool> UpdateUserRoleAsync(string login, int newRoleId);
    Task<bool> DeleteUserAsync(Guid userId);
}