using AutoMapper;
using DataAccess.Repositories.Contracts.Interfaces;
using WriterPlatformWeb.Models.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }
    public async Task<List<GenreDTO>> GetAllGenresAsync()
    {
        var genres = await _genreRepository.GetAllGenresAsync();
        return genres.Select(g => _mapper.Map<GenreDTO>(g)).ToList();
    }

    public async Task<GenreDTO?> GetGenreByIdAsync(int genreId)
    {
        var genreEntity = await _genreRepository.GetGenreByIdAsync(genreId);
        return genreEntity == null ? null : _mapper.Map<GenreDTO>(genreEntity);
    }
}