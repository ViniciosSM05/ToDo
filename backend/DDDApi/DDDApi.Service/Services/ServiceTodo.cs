using AutoMapper;
using DDDApi.Domain.Core.DTO.Todo;
using DDDApi.Domain.Core.Entities;
using DDDApi.Domain.Core.Interfaces.Notification;
using DDDApi.Domain.Core.Interfaces.Repository;
using DDDApi.Domain.Core.Interfaces.Service;
using FluentValidation;

namespace DDDApi.Service.Services
{
    public class ServiceTodo : IServiceTodo
    {
        private readonly IRepositoryTodo repositoryTodo;
        private readonly IMapper mapper;
        private readonly INotification notification;
        private readonly IValidator<TodoSaveDTO> validator;
        public ServiceTodo(IRepositoryTodo repositoryTodo, 
            IMapper mapper, 
            INotification notification, 
            IValidator<TodoSaveDTO> validator)
        {
            this.repositoryTodo = repositoryTodo;
            this.mapper = mapper;
            this.notification = notification;
            this.validator = validator;
        }

        public async Task<TodoSaveResponseDTO> SaveAsync(TodoSaveDTO obj, CancellationToken cancellationToken)
        {
            notification.AddFieldMessages(await validator.ValidateAsync(obj, cancellationToken));
            if (!notification.IsValid) return null;

            var model = mapper.Map<Todo>(obj);

            await (
                model.Id == Guid.Empty 
                    ? repositoryTodo.AddAsync(model, cancellationToken)
                    : repositoryTodo.UpdateAsync(model, cancellationToken)
            );

            return mapper.Map<TodoSaveResponseDTO>(model);
        }

        public async Task<int> RemoveAsync(Guid id, CancellationToken cancellationToken)
        {
            var todo = await repositoryTodo.GetByIdAsync(id, cancellationToken);
            if (todo is null) { notification.AddMessage("Todo não encontrado"); return 0; }

            return await repositoryTodo.RemoveAsync(todo, cancellationToken);
        }

        public async Task<List<TodoGridDTO>> GetTodosByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => await repositoryTodo.GetTodosByUserIdAsync(userId, cancellationToken);

        public async Task<List<TodoCheckDTO>> GetTodosOnPeriodAsync(DateTime initialDate, DateTime finalDate, CancellationToken cancellationToken)
            => await repositoryTodo.GetTodosOnPeriodAsync(initialDate, finalDate, cancellationToken);
    }
}
