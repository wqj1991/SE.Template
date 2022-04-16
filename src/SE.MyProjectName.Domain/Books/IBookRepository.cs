using Hi.Domain.Repositories;

namespace SE.MyProjectName.Domain.Books;

public interface IBookRepository : IRepository<BookEntity, long>
{
    
}