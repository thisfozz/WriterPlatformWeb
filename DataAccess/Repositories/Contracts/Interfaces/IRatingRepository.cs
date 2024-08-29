using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IRatingRepository
{
    Task<RatingEntity> AddRatingAsync(Guid userId, int workId, int ratingValue);
    Task<IEnumerable<RatingEntity>> GetRatingsByWorkIdAsync(int workId);
}