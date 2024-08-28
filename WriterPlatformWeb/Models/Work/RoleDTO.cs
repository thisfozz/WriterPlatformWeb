namespace WriterPlatformWeb.Models.Work;

public class RoleDTO
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public List<UserDTO> Users { get; set; } = new List<UserDTO>();
}