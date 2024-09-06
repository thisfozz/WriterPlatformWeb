namespace WriterPlatformWeb.Models.ViewModel.Work;

public class PaginatedTextViewModel
{
    public int WorkId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int CurrentPage { get; set; } 
    public int TotalPages { get; set; }
}