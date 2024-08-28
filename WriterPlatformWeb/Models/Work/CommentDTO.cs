namespace WriterPlatformWeb.Models.Work;

public class CommentDTO
{
    public int CommentsId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string WorkName { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
}