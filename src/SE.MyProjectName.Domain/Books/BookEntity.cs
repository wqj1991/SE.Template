using Furion;
using SE.Abp.Domain.Entities;
using SE.MyProjectName.Domain.Shared.Books;

namespace SE.MyProjectName.Domain.Books;

public class BookEntity: Entity<long>
{
    private readonly IBookRepository _bookRepository;
    
    public BookEntity()
    {
        // 通过依赖注入框架初始化仓储
        _bookRepository = App.GetService<IBookRepository>();
    }

    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public Decimal Price { get; set; }

    public async Task CreateAsync()
    {
        await _bookRepository.InsertAsync(this);
    }
    
    public async Task UpdateAsync()
    {
        await _bookRepository.UpdateAsync(this);
    }

    public void Publish()
    {
        PublishDate = DateTime.Now;
    }
}