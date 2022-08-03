using DDDApi.Domain.Core.DTO.Todo;

namespace DDDApi.Domain.Core.Interfaces.Service
{
    public interface IServiceTodo
    {
        Task<TodoSaveDTO> Save(TodoSaveDTO obj, CancellationToken cancellationToken);
        Task<int> Remove(Guid id, CancellationToken cancellationToken);
        Task<List<TodoGridDTO>> GetTodosByUserId(Guid userId, CancellationToken cancellationToken);
    }
}
