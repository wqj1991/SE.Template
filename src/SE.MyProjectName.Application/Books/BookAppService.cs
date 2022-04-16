using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Mapster;
using Hi.Application.Contracts.Dtos;
using SE.MyProjectName.Application.Contracts.Books;
using SE.MyProjectName.Application.Contracts.Books.Dtos;
using SE.MyProjectName.Domain.Books;
using SE.MyProjectName.Infrastructure.Books;
using SqlSugar;

namespace SE.MyProjectName.Application.Books;

public class BookAppService :IDynamicApiController, IBookAppService, IScoped
{
    private readonly IBookRepository _bookRepository;
    private readonly ISqlSugarRepository<BookDo> _sqlSugarBookRepository;

    public BookAppService(IBookRepository bookRepository, ISqlSugarRepository<BookDo> sqlSugarBookRepository)
    {
        _bookRepository = bookRepository;
        _sqlSugarBookRepository = sqlSugarBookRepository;
    }

    public async Task<BookDto> Get(long id)
    {
        // 只是获取简单信息建议直接穿透到 Infrastructure 层
        var bookStore = await _sqlSugarBookRepository.FirstOrDefaultAsync(e => e.Id == id);
        return bookStore.Adapt<BookDto>();
    }

    public async Task<PageResultDto<BookDto>> Page(GetBooksInput input)
    {
        // 只是获取简单信息建议直接穿透到 Infrastructure 层
        var items = await _sqlSugarBookRepository.Entities
            .Where(b => b.Name.Contains(input.Filter))
            .OrderBy(input.Sorting)
            .ToPageListAsync(input.CurrentPage, input.PageSize);
        var total = await _sqlSugarBookRepository.CountAsync(b => true);

        return new PageResultDto<BookDto>(total, items.Adapt<List<BookDto>>());
    }


    public async Task Create(CreateBookDto book)
    {
        var bookEntity = book.Adapt<BookEntity>();
        await bookEntity.CreateAsync();
    }

    public async Task Update(UpdateBookDto book)
    {
        var bookEntity = book.Adapt<BookEntity>();
        await bookEntity.UpdateAsync();
    }

    public async Task Delete(long id)
    {
        // 只是获取简单删除建议直接穿透到 Infrastructure 层
        await _sqlSugarBookRepository.DeleteAsync(id);
    }

    public async Task Publish(long id)
    {
        var book = await _bookRepository.GetAsync(id);
        book.Publish();
    }

    public async Task Todo()
    {
        await _bookRepository.GetTodo();
    }
}