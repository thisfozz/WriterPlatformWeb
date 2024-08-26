using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface ICommentRepository
{
    Task<CommentEntity> AddCommentAsync(int userId, int workId, string text);
    Task<IEnumerable<CommentEntity>> GetCommentsByWorkIdAsync(int workId);
    Task DeleteCommentAsync(int commentId);
}