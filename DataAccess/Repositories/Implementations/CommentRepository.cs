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

    public async Task<IEnumerable<CommentEntity>> GetCommentsByWorkIdAsync(int workId)
    {
        return await _context.Comments.Where(args => args.WorksId == workId).ToListAsync();
    }
}