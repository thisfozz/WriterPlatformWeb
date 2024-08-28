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

    public async Task<UserEntity> RegisterUserAsync(string login, string password, string email, int roleId)
    {
        var user = new UserEntity
        {
            Login = login,
            PasswordHash = password,
            Email = email,
            RoleId = roleId
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<UserEntity> AuthenticateUserAsync(string login, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

        if (user == null)
        {
            return null;
        }

        if(user.PasswordHash == password)
        {
            return user;
        }

        return null;
    }

    public async Task<UserEntity> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<UserEntity> GetUserByLoginAsync(string login)
    {
        return await _context.Users.FindAsync(login);
    }

    public async Task<bool> UpdateUserAsync(int userId, string newEmail, string newPassword)
    {
        var user = await GetUserByIdAsync(userId);

        if (user != null)
        {
            user.Email = newEmail;
            user.PasswordHash = newPassword;

            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> UpdateUserRoleAsync(string login, int newRoleId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

        if (user != null)
        {
            user.RoleId = newRoleId;

            await _context.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var searchUser = await _context.Users.FindAsync(userId);
        if (searchUser != null)
        {
            _context.Users.Remove(searchUser);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}