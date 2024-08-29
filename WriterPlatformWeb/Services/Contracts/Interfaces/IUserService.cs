using DataAccess.Entities;
using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IUserService
{
    Task<UserDTO> RegisterUserAsync(string login, string password, string email, int roleId);
    Task<UserDTO?> AuthenticateUserAsync(string login, string password);
    Task<bool> DeleteUserAsync(int userId);
    Task<UserDTO?> GetUserByIdAsync(int userId);
    Task<UserDTO?> GetUserByLoginAsync(string login);
    Task<bool> UpdateUserAsync(int userId, string newEmail, string newPassword);
    Task<bool> UpdateUserRoleAsync(string login, int newRoleId);
}