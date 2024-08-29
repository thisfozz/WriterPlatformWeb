using WriterPlatformWeb.Models.Auth;
using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterModel registerModel);
    Task<bool> AuthenticateUserAsync(LoginModel loginModel);
    Task<bool> Logout();

    Task<bool> DeleteUserAsync(Guid userId);

    Task<UserDTO?> GetUserByIdAsync(Guid userId);
    Task<UserDTO?> GetUserByLoginAsync(string login);

    Task<bool> UpdateUserAsync(Guid userId, string newEmail, string newPassword);
    Task<bool> UpdateUserRoleAsync(string login, int newRoleId);
}