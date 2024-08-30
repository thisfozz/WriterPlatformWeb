using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface ICommentRepository
{
    Task<CommentEntity> AddCommentAsync(int workId, Guid userId, string comment);
    Task<bool> DeleteCommentAsync(int commentId);
    Task<IEnumerable<CommentEntity>> GetCommentsByWorkIdAsync(int workId);
    Task<CommentEntity?> GetCommentByIdAsync(int commentId);
}