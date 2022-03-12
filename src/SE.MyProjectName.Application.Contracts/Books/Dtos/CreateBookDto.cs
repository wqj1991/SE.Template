using SE.MyProjectName.Domain.Shared.Books;

namespace SE.MyProjectName.Application.Contracts.Books.Dtos;

public class CreateBookDto
{
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public Decimal Price { get; set; }
}