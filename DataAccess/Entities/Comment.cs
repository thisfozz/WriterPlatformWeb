namespace DataAccess.Entities;

public partial class Comment
{
    public int CommentsId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = null!;
    public int WorksId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Work Works { get; set; } = null!;
}