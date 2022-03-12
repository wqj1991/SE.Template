using SE.Abp.Application.Contracts.Dtos;
using SE.MyProjectName.Application.Contracts.Books.Dtos;

namespace SE.MyProjectName.Application.Contracts.Books;

public interface IBookAppService
{
    public Task<BookDto> Get(long id);
    
    public Task<PageResultDto<BookDto>> Page(GetBooksInput input);
    
    public Task Create(CreateBookDto book);

    public Task Update(UpdateBookDto book);

    public Task Delete(long id);
}