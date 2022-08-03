using DDDApi.Domain.Core.Interfaces.Repository.Base;
using DDDApi.Domain.Core.Entities;
using DDDApi.Domain.Core.DTO.Todo;

namespace DDDApi.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryTodo : IRepositoryBase<Todo>
    {
        Task<bool> ExistsCodeAsync(string code, Guid userId, CancellationToken cancellationToken);
        Task<bool> ExistsCodeExceptIdAsync(Guid exceptId, string code, Guid userId, CancellationToken cancellationToken);
        Task<List<TodoGridDTO>> GetTodosByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
