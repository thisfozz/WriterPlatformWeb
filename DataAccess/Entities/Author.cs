namespace DataAccess.Entities;

public partial class Author
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}