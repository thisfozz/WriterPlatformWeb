using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IRatingService
{
    Task<RatingDTO> AddRatingAsync(int userId, int workId, int ratingValue);
    Task<List<RatingDTO>> GetRatingsByWorkIdAsync(int workId);
}