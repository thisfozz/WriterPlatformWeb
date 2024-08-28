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

    public async Task<WorkEntity> GetWorkByIdAsync(int workId)
    {
        return await _context.Works.FindAsync(workId);
    }

    public async Task<IEnumerable<WorkEntity>> GetTopWorksByCommentsAsync(int topCount = 50)
    {
        return await _context.Works.OrderByDescending(args => args.Comments.Count).Take(topCount).ToListAsync();
    }

    public async Task<IEnumerable<WorkEntity>> GetTopWorksByRatingAsync(int topCount = 50)
    {
        return await _context.Works.OrderByDescending(args => args.AverageRating).Take(topCount).ToListAsync();
    }

    public async Task<IEnumerable<WorkEntity>> SearchWorksAsync(string authorName, string title, int genreId)
    {
        return await _context.Works
            .Where(args =>
                (string.IsNullOrEmpty(authorName) ||
                 (args.Author.FirstName + " " + args.Author.LastName).Contains(authorName)) &&
                (string.IsNullOrEmpty(title) || args.Title.Contains(title)) &&
                (genreId == 0 || args.GenreId == genreId)
            )
            .ToListAsync();
    }

    public async Task<decimal?> GetCurrentRatingAsync(int workId)
    {
        var work = await _context.Works.FindAsync(workId);

        if(work != null)
        {
            return work.AverageRating;
        }

        return null;
    }

    public async Task<bool> UpdateRatingAsync(int workId, decimal newAverageRating)
    {
        var work = await _context.Works.FindAsync(workId);

        if (work != null)
        {
            work.AverageRating = newAverageRating;
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<CommentEntity> AddCommentAsync(int userId, int workId, string text)
    {
        var comment = new CommentEntity
        {
            UserId = userId,
            Text = text,
            WorksId = workId,
            CreatedAt = DateTime.Now
        };

        var work = await _context.Works.Include(w => w.Comments).FirstOrDefaultAsync(w => w.WorksId == workId);

        if (work != null)
        {
            work.Comments.Add(comment);
        }

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        var searchComment = await _context.Comments.FindAsync(commentId);
        if (searchComment != null)
        {
            _context.Comments.Remove(searchComment);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}