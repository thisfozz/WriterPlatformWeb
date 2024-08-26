using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IGenreRepository
{
    Task<GenreEntity> GetGenreByIdAsync(int genreId);
    Task<IEnumerable<GenreEntity>> GetAllGenresAsync();
}