using Furion.DependencyInjection;
using Hi.Http.Client;
using Hi.Infrastructure.Repositories;
using SE.MyProjectName.Domain.Books;
using SE.OtherProjectName.Application.Contracts.Todos;
using SqlSugar;

namespace SE.MyProjectName.Infrastructure.Books;

public class BookRepository : Repository<BookEntity, BookDo, long>, IBookRepository, IScoped
{
    private readonly ITodoAppService _todoProxy;

    public BookRepository(ISqlSugarRepository<BookDo> sqlSugarRepository,
        ITodoAppService todoProxy) : base(sqlSugarRepository)
    {
        _todoProxy = todoProxy;
    }

    public async Task GetTodo()
    {
        var aa = await _todoProxy.Get(1);
    }
}