using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IGenreService
{
    Task<GenreDTO?> GetGenreByIdAsync(int genreId);
    Task<List<GenreDTO>> GetAllGenresAsync();
}