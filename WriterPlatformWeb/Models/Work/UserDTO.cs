namespace WriterPlatformWeb.Models.Work;

public class UserDTO
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool? IsDeleted { get; set; }
    public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    public List<RatingDTO> Ratings { get; set; } = new List<RatingDTO>();
}