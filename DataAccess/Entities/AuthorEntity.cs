namespace DataAccess.Entities;

public partial class AuthorEntity
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public virtual ICollection<WorkEntity> Works { get; set; } = new List<WorkEntity>();
}