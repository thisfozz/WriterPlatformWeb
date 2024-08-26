namespace DataAccess.Entities;

public partial class RoleEntity
{
    public int RoleId { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}