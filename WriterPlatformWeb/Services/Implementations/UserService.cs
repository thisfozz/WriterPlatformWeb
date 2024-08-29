using AutoMapper;
using DataAccess.Repositories.Contracts.Interfaces;
using WriterPlatformWeb.Models.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDTO> RegisterUserAsync(string login, string password, string email, int roleId)
    {
        var user = await _userRepository.RegisterUserAsync(login, password, email, roleId);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO?> AuthenticateUserAsync(string login, string password)
    {
        var user = await _userRepository.AuthenticateUserAsync(login, password);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        return await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<UserDTO?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO?> GetUserByLoginAsync(string login)
    {
        var user = await _userRepository.GetUserByLoginAsync(login);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<bool> UpdateUserAsync(int userId, string newEmail, string newPassword)
    {
        return await _userRepository.UpdateUserAsync(userId, newEmail, newPassword);
    }

    public async Task<bool> UpdateUserRoleAsync(string login, int newRoleId)
    {
        return await _userRepository.UpdateUserRoleAsync(login, newRoleId);
    }
}