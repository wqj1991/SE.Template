using Furion;
using Hi.Domain.Entities;
using MapsterMapper;
using SE.MyProjectName.Domain.Shared.Books;

namespace SE.MyProjectName.Domain.Books;

public class BookEntity : Entity<long>
{
    private readonly IBookRepository _bookRepository;

    public BookEntity()
    {
        // 通过依赖注入框架初始化仓储
        _bookRepository = App.GetService<IBookRepository>();
    }

    public BookEntity(long id) : base(id)
    {
        // 通过依赖注入框架初始化仓储
        _bookRepository = App.GetService<IBookRepository>();
        var mapper = App.GetService<IMapper>();
        var entity = _bookRepository.GetAsync(id).Result;

        mapper.Map(entity, this);
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
    
    public async Task DeleteAsync()
    {
        await _bookRepository.DeleteAsync(Id);
    }

    public async Task PublishAsync()
    {
        PublishDate = DateTime.Now;
        await _bookRepository.UpdateAsync(this);
    }
}