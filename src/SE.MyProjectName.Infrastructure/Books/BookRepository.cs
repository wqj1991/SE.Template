using Furion.DependencyInjection;
using SE.Abp.Infrastructure.Repositories;
using SE.MyProjectName.Domain.Books;
using SqlSugar;

namespace SE.MyProjectName.Infrastructure.Books;

public class BookRepository: Repository<BookEntity, BookStore, long>, IBookRepository, IScoped
{
    public BookRepository(ISqlSugarRepository<BookStore> sqlSugarRepository) : base(sqlSugarRepository)
    {
    }
}