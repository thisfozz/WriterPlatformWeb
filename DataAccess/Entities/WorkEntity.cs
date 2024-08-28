namespace DataAccess.Entities;

public partial class WorkEntity
{
    public int WorksId { get; set; }
    public string Title { get; set; } = null!;
    public int GenreId { get; set; }
    public int AuthorId { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string Text { get; set; } = null!;
    public decimal? AverageRating { get; set; }
    public virtual AuthorEntity Author { get; set; } = null!;
    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    public virtual GenreEntity Genre { get; set; } = null!;
    public virtual ICollection<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
}