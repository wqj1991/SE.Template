using Furion.RemoteRequest;
using SE.Application.Contracts.Dtos;
using SE.OtherProjectName.Application.Contracts.Todos.Dtos;

namespace SE.OtherProjectName.Application.Contracts.Todos;

public interface ITodoAppService
{
    public Task<TodoDto> Get(long id);
    
    public Task<PageResultDto<TodoDto>> Page(GetTodosInput input);
    
    public Task Create(CreateTodoDto todo);

    public Task Update(UpdateTodoDto todo);

    public Task Delete(long id);

    public Task Publish(long id);
}