using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class AuthorRepository : IAuthorRepository
{
    private readonly WriterPlatformContext _context;

    public AuthorRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<AuthorEntity> AddAuthorAsync(string firstName, string lastName)
    {
        var author = new AuthorEntity
        {
            FirstName = firstName,
            LastName = lastName
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task<bool> DeleteAuthorAsync(int authorId)
    {
        var author = await GetAuthorByIdAsync(authorId);

        if (author == null)
        {
            return false;
        }
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateAuthorAsync(int authorId, string firstName, string lastName)
    {
        var author = await GetAuthorByIdAsync(authorId);

        if (author == null)
        {
            return false;
        }

        author.FirstName = firstName;
        author.LastName = lastName;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<AuthorEntity>> GetAllAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<AuthorEntity?> GetAuthorByIdAsync(int authorId)
    {
        return await _context.Authors.FindAsync(authorId);
    }

    public async Task<IEnumerable<AuthorEntity>> SearchAuthorsByFullNameAsync(string fullName)
    {
        var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (names.Length == 2)
        {
            var firstName = names[0];
            var lastName = names[1];

            return await _context.Authors
                .Where(a => a.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                            a.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        return Enumerable.Empty<AuthorEntity>();
    }
}