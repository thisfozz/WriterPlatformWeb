using AutoMapper;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WriterPlatformWeb.Helpers;
using WriterPlatformWeb.Models.Auth;
using WriterPlatformWeb.Models.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _roleRepository = roleRepository;
    }

    public async Task<bool> RegisterUserAsync(RegisterModel registerModel)
    {
        if (await _userRepository.IsUserRegisteredByLoginAsync(registerModel.Login))
        {
            return false;
        }

        var userId = Guid.NewGuid();
        var hashedPassword = SHA256Manager.GenerateSaltedHash(registerModel.Password, userId.ToString());
        var userEntity = new UserEntity
        {
            UserId = userId,
            Login = registerModel.Login,
            PasswordHash = hashedPassword,
            RoleId = 1 //Дефолтный ID роли для новых пользователей
        };

        var role = await _roleRepository.GetRoleByIdAsync(userEntity.RoleId);
        var roleName = role?.Name ?? "User";

        await _userRepository.RegisterUserAsync(userEntity);
        await SetupUserAuthenticationAsync(registerModel.Login, registerModel.Email, roleName);

        return true;
    }

    public async Task<bool> AuthenticateUserAsync(LoginModel loginModel)
    {
        var user = await _userRepository.GetUserByLoginOrEmailAsync(loginModel.LoginOrEmail);

        return user != null && SHA256Manager.PasswordMath(loginModel.Password, user.UserId.ToString(), user.PasswordHash);
    }

    public async Task<bool> LogoutAsync()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return true;
    }

    public async Task<bool> DeleteUserAsync()
    {
        var userId = GetCurrentUserId();
        return await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<UserDTO?> GetUserByIdAsync()
    {
        var userId = GetCurrentUserId();
        var user = await _userRepository.GetUserByIdAsync(userId);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<bool> UpdateEmailAsync(string newEmail)
    {
        var userId = GetCurrentUserId();
        return await _userRepository.UpdateEmailAsync(userId, newEmail);
    }

    public async Task<bool> UpdatePasswordAsync(string newPassword)
    {
        var userId = GetCurrentUserId();
        return await _userRepository.UpdatePasswordAsync(userId, newPassword);
    }

    public async Task<bool> UpdateUserRoleAsync(string login, int newRoleId)
    {
        var roleUpdated = await _userRepository.UpdateUserRoleAsync(login, newRoleId);
        if (roleUpdated)
        {
            return await UpdateUserRoleClaimsAsync(login, newRoleId);
        }

        return false;
    }

    private async Task SetupUserAuthenticationAsync(string login, string email, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Email, email)
        };
        var identity = new ClaimsIdentity(claims, "ApplicationCookie");

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
    }

    private async Task<bool> UpdateUserRoleClaimsAsync(string loginOrEmail, int newRoleId)
    {
        var user = await _userRepository.GetUserByLoginOrEmailAsync(loginOrEmail);
        if (user == null) return false;

        var role = await _roleRepository.GetRoleByIdAsync(newRoleId);
        if (role == null) return false;

        var claims = new[]
        {
            new Claim(ClaimTypes.Role, role.Name)
        };
        var identity = new ClaimsIdentity(claims, "ApplicationCookie");

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        return true;
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            return userId;
        }
        throw new UnauthorizedAccessException("User is not authenticated");
    }
}
