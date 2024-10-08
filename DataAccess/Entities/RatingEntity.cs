﻿namespace DataAccess.Entities;

public partial class RatingEntity
{
    public int RatingId { get; set; }
    public Guid UserId { get; set; }
    public int WorksId { get; set; }
    public int? RatingValue { get; set; }
    public virtual UserEntity User { get; set; } = null!;
    public virtual WorkEntity Works { get; set; } = null!;
}