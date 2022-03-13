namespace SE.OtherProjectName.Application.Contracts.Todos.Dtos;

public class GetTodosInput
{
    public string Filter { get; set; }
    
    public string  Sorting { get; set; }

    public int PageSize { get; set; } = 20;

    public int CurrentPage { get; set; } = 1;
}