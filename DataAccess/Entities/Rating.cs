namespace DataAccess.Entities;

public partial class Rating
{
    public int RatingId { get; set; }
    public int UserId { get; set; }
    public int WorksId { get; set; }
    public int? RatingValue { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Work Works { get; set; } = null!;
}