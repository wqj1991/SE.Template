namespace SE.MyProjectName.Application.Contracts.Books.Dtos;

public class GetBooksInput
{
    public string Filter { get; set; }
    
    public string  Sorting { get; set; }

    public int PageSize { get; set; } = 20;

    public int CurrentPage { get; set; } = 1;
}