namespace DataAccess.Entities;

public partial class Genre
{
    public int GenreId { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}