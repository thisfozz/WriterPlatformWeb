using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class GenreRepository : IGenreRepository
{
    private readonly WriterPlatformContext _context;

    public GenreRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GenreEntity>> GetAllGenresAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<GenreEntity> GetGenreByIdAsync(int genreId)
    {
        throw new NotImplementedException();
    }
}