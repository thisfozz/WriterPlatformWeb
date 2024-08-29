using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IAuthorRepository
{
    Task<AuthorEntity> AddAuthorAsync(string name, string lastName);
    Task<bool> DeleteAuthorAsync(int authorId);
    Task<bool> UpdateAuthorAsync(int authorId, string firstName, string lastName);
    Task<AuthorEntity?> GetAuthorByIdAsync(int authorId);
    Task<IEnumerable<AuthorEntity>> GetAllAuthorsAsync();
    Task<IEnumerable<AuthorEntity>> SearchAuthorsAsync(string fistnameOrLastname);
}