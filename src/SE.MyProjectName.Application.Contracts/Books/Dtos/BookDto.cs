using SE.MyProjectName.Domain.Shared.Books;

namespace SE.MyProjectName.Application.Contracts.Books.Dtos;

public class BookDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public Decimal Price { get; set; }
}