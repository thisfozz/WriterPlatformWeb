using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class RatingRepository : IRatingRepository
{
    private readonly WriterPlatformContext _context;

    public RatingRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<Rating> AddRatingAsync(int userId, int workId, int ratingValue)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Rating>> GetRatingsByWorkIdAsync(int workId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateRatingAsync(int ratingId, int ratingValue)
    {
        throw new NotImplementedException();
    }
}