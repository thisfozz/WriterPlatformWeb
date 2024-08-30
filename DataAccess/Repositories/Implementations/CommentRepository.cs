using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class CommentRepository : ICommentRepository
{
    private readonly WriterPlatformContext _context;

    public CommentRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<CommentEntity> AddCommentAsync(int workId, Guid userId, string comment)
    {
        var newComment = new CommentEntity
        {
            UserId = userId,
            WorksId = workId,
            Text = comment,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(newComment);
        await _context.SaveChangesAsync();

        return newComment;
    }

    public async Task<IEnumerable<CommentEntity>> GetCommentsByWorkIdAsync(int workId)
    {
        return await _context.Comments.Where(comment => comment.WorksId == workId).ToListAsync();
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId);

        if (comment == null)
        {
            return false;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<CommentEntity?> GetCommentByIdAsync(int commentId)
    {
        return await _context.Comments.Where(c => c.CommentsId == commentId).FirstOrDefaultAsync();
    }
}