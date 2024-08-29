using AutoMapper;
using DataAccess.Repositories.Contracts.Interfaces;
using WriterPlatformWeb.Models.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }
    public async Task<AuthorDTO> AddAuthorAsync(string firstName, string lastName)
    {
        var author = await _authorRepository.AddAuthorAsync(firstName, lastName);
        return _mapper.Map<AuthorDTO>(author);
    }

    public async Task<bool> DeleteAuthorAsync(int authorId)
    {
        return await _authorRepository.DeleteAuthorAsync(authorId);
    }

    public async Task<bool> UpdateAuthorAsync(int authorId, string firstName, string lastName)
    {
        return await _authorRepository.UpdateAuthorAsync(authorId, firstName, lastName);
    }

    public async Task<AuthorDTO?> GetAuthorByIdAsync(int authorId)
    {
        var author = await _authorRepository.GetAuthorByIdAsync(authorId);
        return author == null ? null : _mapper.Map<AuthorDTO>(author);
    }

    public async Task<List<AuthorDTO>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAuthorsAsync();
        return authors.Select(a => _mapper.Map<AuthorDTO>(a)).ToList();
    }

    public async Task<List<AuthorDTO>> SearchAuthorsAsync(string fistnameOrLastname)
    {
        var authors = await _authorRepository.SearchAuthorsAsync(fistnameOrLastname);
        return authors.Select(a => _mapper.Map<AuthorDTO>(a)).ToList();
    }
}