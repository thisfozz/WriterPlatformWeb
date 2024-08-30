using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class GenreRepository : IGenreRepository
{
    private readonly WriterPlatformContext _context;

    public GenreRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateGenreAsync(string genreName)
    {
        var genre = new GenreEntity { Name = genreName };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<GenreEntity?> GetGenreByIdAsync(int genreId)
    {
        return await _context.Genres.FindAsync(genreId);
    }

    public async Task<IEnumerable<GenreEntity>> GetAllGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }
}