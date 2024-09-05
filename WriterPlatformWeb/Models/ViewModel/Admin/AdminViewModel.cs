using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Models.ViewModel.Admin;

public class AdminViewModel
{
    public List<UserDTO> Users { get; set; }
    public List<RoleDTO> Roles { get; set; }
    public List<AuthorDTO> Authors { get; set; }
}