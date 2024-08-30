using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IGenreService
{
    Task<bool> CreateGenreAsync(string genreName);
    Task<GenreDTO?> GetGenreByIdAsync(int genreId);
    Task<List<GenreDTO>> GetAllGenresAsync();
}