namespace DataAccess.Entities;

public partial class GenreEntity
{
    public int GenreId { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<WorkEntity> Works { get; set; } = new List<WorkEntity>();
}