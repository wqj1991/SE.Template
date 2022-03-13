using Furion.DependencyInjection;
using SE.Http.Client;
using SE.Infrastructure.Repositories;
using SE.MyProjectName.Domain.Books;
using SE.OtherProjectName.Application.Contracts.Todos;
using SqlSugar;

namespace SE.MyProjectName.Infrastructure.Books;

public class BookRepository : Repository<BookEntity, BookStore, long>, IBookRepository, IScoped
{
    private readonly ITodoAppService _todoProxy;

    public BookRepository(ISqlSugarRepository<BookStore> sqlSugarRepository,
        ITodoAppService todoProxy) : base(sqlSugarRepository)
    {
        _todoProxy = todoProxy;
    }

    public async Task GetTodo()
    {
        var aa = await _todoProxy.Get(1);
    }
}