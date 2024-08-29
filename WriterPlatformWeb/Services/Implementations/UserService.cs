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

    public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _roleRepository = roleRepository;
    }

    public async Task<bool> RegisterUserAsync(RegisterModel registerModel)
    {
        var isUserRegistered = await _userRepository.IsUserRegisteredByLoginAsync(registerModel.Login);

        if (!isUserRegistered)
        {
            var id = Guid.NewGuid();

            var userEntity = new UserEntity
            {
                UserId = id,
                Login = registerModel.Login,
                PasswordHash = SHA256Manager.GenerateSaltedHash(registerModel.Password, id.ToString()),
                RoleId = 1
            };

            var roleEntity = await _roleRepository.GetRoleByIdAsync(userEntity.RoleId);
            var role = roleEntity?.Name ?? "User";

            await _userRepository.RegisterUserAsync(userEntity);
            await SetupUserAuthenticationAsync(registerModel.Login, registerModel.Email, role);

            return true;
        }

        return false;
    }

    public async Task<bool> AuthenticateUserAsync(LoginModel loginModel)
    {
        var user = await _userRepository.GetUserByLoginOrEmailAsync(loginModel.LoginOrEmail);

        if (user != null && SHA256Manager.PasswordMath(loginModel.Password, user.UserId.ToString(), user.PasswordHash))
        {
            return true;
        }
        return false;
    }

    public async Task<bool> Logout()
    {
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        return await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<UserDTO?> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO?> GetUserByLoginAsync(string login)
    {
        var user = await _userRepository.GetUserByLoginOrEmailAsync(login);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<bool> UpdateUserAsync(Guid userId, string newEmail, string newPassword)
    {
        var passwordHash = SHA256Manager.GenerateSaltedHash(newPassword, userId.ToString());
        return await _userRepository.UpdateUserAsync(userId, newEmail, newPassword);
    }

    public async Task<bool> UpdateUserRoleAsync(string login, int newRoleId)
    {
        bool roleUpdated = await _userRepository.UpdateUserRoleAsync(login, newRoleId);

        if(roleUpdated)
        {
            return await UpdateUserRoleAsync(login, newRoleId);
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
        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    private async Task<bool> UpdateUserRoleClaimsAsync(string loginOrEmail, int newRoleId)
    {
        var user = await _userRepository.GetUserByLoginOrEmailAsync(loginOrEmail);

        if(user == null)
        {
            return false;
        }
        var role = await _roleRepository.GetRoleByIdAsync(newRoleId);

        if(role == null )
        {
            return false;
        }

        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        return true;
    }
}