namespace DataAccess.Entities;

public partial class CommentEntity
{
    public int CommentsId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = null!;
    public int WorksId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public virtual UserEntity User { get; set; } = null!;
    public virtual WorkEntity Works { get; set; } = null!;
}