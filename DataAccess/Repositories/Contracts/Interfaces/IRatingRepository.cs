using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IRatingRepository
{
    Task<Rating> AddRatingAsync(int userId, int workId, int ratingValue);
    Task<IEnumerable<Rating>> GetRatingsByWorkIdAsync(int workId);
    Task UpdateRatingAsync(int ratingId, int ratingValue);
}