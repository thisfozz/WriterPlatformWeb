using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class CommentRepository : ICommentRepository
{
    private readonly WriterPlatformContext _context;

    public CommentRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<CommentEntity> AddCommentAsync(int userId, int workId, string text)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CommentEntity>> GetCommentsByWorkIdAsync(int workId)
    {
        throw new NotImplementedException();
    }
}