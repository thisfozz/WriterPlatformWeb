using AutoMapper;
using DataAccess.Repositories.Contracts.Interfaces;
using WriterPlatformWeb.Models.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Services.Implementations;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IMapper _mapper;

    public RatingService(IRatingRepository ratingRepository, IMapper mapper)
    {
        _ratingRepository = ratingRepository;
        _mapper = mapper;
    }
    public async Task<RatingDTO> AddRatingAsync(Guid userId, int workId, int ratingValue)
    {
        return _mapper.Map<RatingDTO>(await _ratingRepository.AddRatingAsync(userId, workId, ratingValue));
    }

    public async Task<List<RatingDTO>> GetRatingsByWorkIdAsync(int workId)
    {
        var ratings = await _ratingRepository.GetRatingsByWorkIdAsync(workId);
        return ratings.Select(r => _mapper.Map<RatingDTO>(r)).ToList();
    }
}