using DDDApi.Application.Applications.Base;
using DDDApi.Domain.Core.DTO.Todo;
using DDDApi.Domain.Core.Interfaces.Application;
using DDDApi.Domain.Core.Interfaces.Email;
using DDDApi.Domain.Core.Interfaces.Queue;
using DDDApi.Domain.Core.Interfaces.Service;

namespace DDDApi.Application.Applications
{
    public class ApplicationTodo : ApplicationBase, IApplicationTodo
    {
        private readonly IServiceTodo serviceTodo;
        private readonly ISendEmailBuilder sendEmailBuilder;
        private readonly IEmailClient emailClient;

        public ApplicationTodo(
            IServiceTodo serviceTodo, 
            ISendEmailBuilder sendEmailBuilder,
            IEmailClient emailClient)
        {
            this.serviceTodo = serviceTodo;
            this.sendEmailBuilder = sendEmailBuilder;
            this.emailClient = emailClient;
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

        public async Task CheckNextTodosAsync(CancellationToken cancellationToken)
        {          
            var initialDate = DateTime.UtcNow;
            var finalDate = initialDate.AddMinutes(5);

            var nextTodos = await serviceTodo.GetTodosOnPeriodAsync(initialDate, finalDate, cancellationToken);
            foreach (var todo in nextTodos)
            {
                sendEmailBuilder.Clear();
                var emailToSend = sendEmailBuilder
                    .WithSubject("UMA NOVA TAREFA EM INSTANTES!")
                    .WithRecipient(todo.UserEmail)
                    .WithBodyHTML($"<h1>Olá {todo.UserName}, seu ToDo {todo.Code} - {todo.Description} está próximo do horário marcado! :D</h1>")
                    .Build();
                await emailClient.PostEmailOnQueueAsync(emailToSend);
            }            
        }
    }
}
