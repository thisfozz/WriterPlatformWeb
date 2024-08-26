namespace DataAccess.Entities;

public partial class Work
{
    public int WorksId { get; set; }
    public string Title { get; set; } = null!;
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string Text { get; set; } = null!;
    public decimal? AverageRating { get; set; }
    public virtual Author Author { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual Genre Genre { get; set; } = null!;
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}