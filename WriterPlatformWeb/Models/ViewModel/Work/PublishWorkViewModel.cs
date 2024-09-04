using Microsoft.AspNetCore.Mvc.Rendering;
using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Models.ViewModel.Work;

public class PublishWorkViewModel
{
    public WorkDTO Work { get; set; } = new WorkDTO();
    public IEnumerable<AuthorDTO> Authors { get; set; } = new List<AuthorDTO>();
    public IEnumerable<GenreDTO> Genres { get; set; } = new List<GenreDTO>();

    public int SelectedAuthorId { get; set; }
    public int SelectedGenreId { get; set; }
}