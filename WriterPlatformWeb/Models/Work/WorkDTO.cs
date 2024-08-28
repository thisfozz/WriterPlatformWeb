namespace WriterPlatformWeb.Models.Work;

public class WorkDTO
{
    public int WorkId { get; set; }
    public string Title { get; set; }
    public string GenreName { get; set; }
    public string AuthorName { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string Text { get; set; }
    public decimal? AverageRating { get; set; }
    public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    public List<RatingDTO> Ratings { get; set; } = new List<RatingDTO>();
    public AuthorDTO Author { get; set; }
    public GenreDTO Genre { get; set; }
}