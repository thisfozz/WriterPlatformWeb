using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class RatingRepository : IRatingRepository
{
    private readonly WriterPlatformContext _context;

    public RatingRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<RatingEntity> AddRatingAsync(Guid userId, int workId, int ratingValue)
    {
        var rating = new RatingEntity
        {
            UserId = userId,
            WorksId = workId,
            RatingValue = ratingValue
        };

        var work = await _context.Works.Include(w => w.Ratings).FirstOrDefaultAsync(w => w.WorksId == workId);

        if(work != null)
        {
            work.Ratings.Add(rating);
        }

        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();

        return rating;
    }

    public async Task<IEnumerable<RatingEntity>> GetRatingsByWorkIdAsync(int workId)
    {
        return await _context.Ratings.Where(args => args.WorksId == workId).ToListAsync();
    }
}