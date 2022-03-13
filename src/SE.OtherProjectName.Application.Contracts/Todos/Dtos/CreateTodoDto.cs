namespace SE.OtherProjectName.Application.Contracts.Todos.Dtos;

public class CreateTodoDto
{
    public string Name { get; set; }

    public DateTime PublishDate { get; set; }

    public Decimal Price { get; set; }
}