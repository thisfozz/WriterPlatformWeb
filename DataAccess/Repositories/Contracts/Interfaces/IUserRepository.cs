using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IUserRepository
{
    Task<UserEntity> RegisterUserAsync(UserEntity user); // РЕГИСТРАЦИЯ ПОЛЬЗОВАТЕЛЯ
    Task<bool> IsUserRegisteredByLoginAsync(string login); // ПРОВЕРКА ЗАРЕГИСТРИРОВАН ЛИ ПОЛЬЗОВАТЕЛЬ

    Task<UserEntity?> GetUserByIdAsync(Guid userId);
    Task<UserEntity?> GetUserByLoginOrEmailAsync(string loginOrEmail);
    Task<bool> UpdateUserAsync(Guid userId, string newEmail, string newPassword);
    Task<bool> UpdateUserRoleAsync(string login, int newRoleId);
    Task<bool> DeleteUserAsync(Guid userId);
}