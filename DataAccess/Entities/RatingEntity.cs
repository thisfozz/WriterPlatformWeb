namespace DataAccess.Entities;

public partial class Rating
{
    public int RatingId { get; set; }
    public int UserId { get; set; }
    public int WorksId { get; set; }
    public int? RatingValue { get; set; }
    public virtual UserEntity User { get; set; } = null!;
    public virtual WorkEntity Works { get; set; } = null!;
}