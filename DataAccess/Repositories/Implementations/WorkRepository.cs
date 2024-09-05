using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class WorkRepository : IWorkRepository
{
    private readonly WriterPlatformContext _context;

    public WorkRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<WorkEntity> PublishWorkAsync(string title, int genreId, int authorId, DateOnly publicationDate, string text)
    {
        var work = new WorkEntity
        {
            Title = title,
            GenreId = genreId,
            AuthorId = authorId,
            PublicationDate = publicationDate,
            Text = text
        };

        _context.Works.Add(work);
        await _context.SaveChangesAsync();

        return work;
    }

    public async Task<WorkEntity?> GetWorkByIdAsync(int workId)
    {
        return await _context.Works
            .Include(w => w.Author)
            .Include(w => w.Genre)
            .Include(w => w.Comments)
            .Include(w => w.Ratings)
            .FirstOrDefaultAsync(w => w.WorksId == workId);
    }

    public async Task<IEnumerable<WorkEntity>> GetTopWorksByCommentsAsync(int topCount = 50)
    {
        return await _context.Works.OrderByDescending(work => work.Comments.Count).Take(topCount).ToListAsync();
    }

    public async Task<IEnumerable<WorkEntity>> GetTopWorksByRatingAsync(int topCount = 50)
    {
        return await _context.Works.OrderByDescending(work => work.AverageRating).Take(topCount).ToListAsync();
    }

    public async Task<IEnumerable<WorkEntity>> SearchWorksAsync(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return await _context.Works.ToListAsync();
        }

        var query = _context.Works.AsQueryable();

        query = query.Where(work => work.Title.Contains(value));

        query = query.Where(work => (work.Author.FirstName).Contains(value) || (work.Author.LastName).Contains(value) || (work.Author.FirstName + " " + work.Author.LastName).Contains(value));

        query = query.Where(work => work.Genre.Name.Contains(value));

        return await query.ToListAsync();
    }

    public async Task<decimal?> GetCurrentRatingAsync(int workId)
    {
        var work = await _context.Works.FindAsync(workId);

        return work?.AverageRating;
    }

    public async Task<bool> UpdateRatingAsync(int workId, decimal newAverageRating)
    {
        var work = await _context.Works.FindAsync(workId);

        if (work == null)
        {
            return false;
        }

        work.AverageRating = newAverageRating;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<WorkEntity>> GetAllWorksAsync()
    {
        return await _context.Works
            .Include(w => w.Author)
            .Include(w => w.Genre)
            .Include(w => w.Comments)
            .Include(w => w.Ratings)
            .ToListAsync();
    }
}