﻿using WriterPlatformWeb.Models.Auth;
using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterModel registerModel);
    Task<bool> AuthenticateUserAsync(LoginModel loginModel);
    Task<bool> LogoutAsync();

    Task<bool> DeleteUserAsync();

    Task<UserDTO?> GetUserByIdAsync();

    Task<bool> UpdateEmailAsync(string newEmail);
    Task<bool> UpdatePasswordAsync(string password);
    Task<bool> UpdateUserRoleAsync(string login, int newRoleId);
}