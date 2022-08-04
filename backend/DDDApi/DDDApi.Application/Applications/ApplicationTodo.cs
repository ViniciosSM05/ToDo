using DDDApi.Application.Applications.Base;
using DDDApi.Domain.Core.DTO.Todo;
using DDDApi.Domain.Core.Interfaces.Application;
using DDDApi.Domain.Core.Interfaces.Service;

namespace DDDApi.Application.Applications
{
    public class ApplicationTodo : ApplicationBase, IApplicationTodo
    {
        private readonly IServiceTodo serviceTodo;
        public ApplicationTodo(IServiceTodo serviceTodo)
        {
            this.serviceTodo = serviceTodo;
        }

        public async Task<TodoSaveResponseDTO> SaveAsync(TodoSaveDTO obj, CancellationToken cancellationToken)
        {
            using var transacao = TransactionScopeAsync();
            var response = await serviceTodo.SaveAsync(obj, cancellationToken);
            transacao.Complete();

            return response;
        }

        public async Task<int> RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            using var transacao = TransactionScopeAsync();
            var response = await serviceTodo.RemoveAsync(id, cancellationToken);
            transacao.Complete();

            return response;
        }

        public async Task<List<TodoGridDTO>> GetTodosByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => await serviceTodo.GetTodosByUserIdAsync(userId, cancellationToken);
    }
}
