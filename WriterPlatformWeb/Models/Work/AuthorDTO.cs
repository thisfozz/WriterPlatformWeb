namespace WriterPlatformWeb.Models.Work;

public class AuthorDTO
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; } = string.Empty!;
    public string LastName { get; set; } = string.Empty!;
    public List<WorkDTO> Works { get; set; } = new List<WorkDTO>();
}