using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IAuthorService
{
    Task<AuthorDTO> AddAuthorAsync(string firstName, string lastName);
    Task<bool> DeleteAuthorAsync(int authorId);
    Task<bool> UpdateAuthorAsync(int authorId, string firstName, string lastName);
    Task<AuthorDTO?> GetAuthorByIdAsync(int authorId);
    Task<List<AuthorDTO>> GetAllAuthorsAsync();
    Task<List<AuthorDTO>> SearchAuthorsAsync(string fistnameOrLastname);
}