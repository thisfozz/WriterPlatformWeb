using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Models.ViewModel;
public class WorkDetailsViewModel
{
    public WorkDTO Work { get; set; }
    public CommentDTO[] Comments { get; set; }
}
