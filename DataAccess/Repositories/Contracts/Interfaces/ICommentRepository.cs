using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<CommentEntity>> GetCommentsByWorkIdAsync(int workId);
}