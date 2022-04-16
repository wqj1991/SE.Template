using Furion.DependencyInjection;
using Hi.Http.Client;
using Hi.Infrastructure.Repositories;
using SE.MyProjectName.Domain.Books;
using SqlSugar;

namespace SE.MyProjectName.Infrastructure.Books;

public class BookRepository : Repository<BookEntity, BookDo, long>, IBookRepository, IScoped
{

    public BookRepository(ISqlSugarRepository<BookDo> sqlSugarRepository) : base(sqlSugarRepository)
    {
    }
}