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

        public async Task<TodoSaveDTO> Save(TodoSaveDTO obj, CancellationToken cancellationToken)
        {
            using var transacao = TransactionScopeAsync();
            var response = await serviceTodo.Save(obj, cancellationToken);
            transacao.Complete();

            return response;
        }

        public async Task<int> Remove(Guid id, CancellationToken cancellationToken)
        {
            using var transacao = TransactionScopeAsync();
            var response = await serviceTodo.Remove(id, cancellationToken);
            transacao.Complete();

            return response;
        }

        public async Task<List<TodoGridDTO>> GetTodosByUserId(Guid userId, CancellationToken cancellationToken)
            => await serviceTodo.GetTodosByUserId(userId, cancellationToken);
    }
}
