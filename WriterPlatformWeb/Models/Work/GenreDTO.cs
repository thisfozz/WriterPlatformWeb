namespace WriterPlatformWeb.Models.Work;

public class GenreDTO
{
    public int GenreId { get; set; }
    public string GenreName { get; set;} = string.Empty!;
    public List<WorkDTO> Works { get; set; } = new List<WorkDTO>();
}