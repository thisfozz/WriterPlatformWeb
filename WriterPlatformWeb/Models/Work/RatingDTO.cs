namespace WriterPlatformWeb.Models.Work;

public class RatingDTO
{
    public int RatingId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string WorkName {  get; set; } = string.Empty;
    public int? RatingValue { get; set; }

    public UserDTO User { get; set; }
    public WorkDTO Work { get; set; }
}