using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly WriterPlatformContext _context;

    public UserRepository(WriterPlatformContext context)
    {
        _context = context;
    }
    public async Task<bool> IsUserRegisteredByLoginAsync(string login)
    {
        return await _context.Users.AnyAsync(u => u.Login == login);
    }

    public async Task<UserEntity> RegisterUserAsync(UserEntity user)
    {
        var isUserRegistered = await IsUserRegisteredByLoginAsync(user.Login);

        if (isUserRegistered)
        {
            throw new InvalidOperationException("Пользователь с таким логином уже зарегистрирован.");
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<UserEntity?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<UserEntity?> GetUserByLoginOrEmailAsync(string loginOrEmail)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
    }

    public async Task<bool> UpdateEmailAsync(Guid userId, string newEmail)
    {
        var user = await GetUserByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        user.Email = newEmail;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdatePasswordAsync(Guid userId, string password)
    {
        var user = await GetUserByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        user.PasswordHash = password;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateUserRoleAsync(string login, int newRoleId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

        if (user == null)
        {
            return false;
        }

        user.RoleId = newRoleId;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await GetUserByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}