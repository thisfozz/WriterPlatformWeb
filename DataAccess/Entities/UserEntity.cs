namespace DataAccess.Entities;

public partial class UserEntity
{
    public Guid UserId { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int RoleId { get; set; }
    public bool? IsDeleted { get; set; }
    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    public virtual ICollection<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
    public virtual RoleEntity Role { get; set; } = null!;
}