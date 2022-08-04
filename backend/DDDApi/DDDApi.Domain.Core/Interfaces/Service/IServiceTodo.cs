using DDDApi.Domain.Core.DTO.Todo;

namespace DDDApi.Domain.Core.Interfaces.Service
{
    public interface IServiceTodo
    {
        Task<TodoSaveResponseDTO> SaveAsync(TodoSaveDTO obj, CancellationToken cancellationToken);
        Task<int> RemoveAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TodoGridDTO>> GetTodosByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
