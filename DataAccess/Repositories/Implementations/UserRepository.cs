using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly WriterPlatformContext _context;

    public UserRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> AuthenticateUserAsync(string login, string password)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> GetUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> GetUserByLoginAsync(string login)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> RegisterUserAsync(string login, string password, string email)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateUserAsync(int userId, string newEmail, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateUserRoleAsync(string login, int newRoleId)
    {
        throw new NotImplementedException();
    }
}