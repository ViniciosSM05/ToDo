using DDDApi.Domain.Core.DTO.Todo;
using DDDApi.Domain.Core.Interfaces.Repository;
using FluentValidation;

namespace DDDApi.Domain.Validators
{
    public class TodoSaveValidator : AbstractValidator<TodoSaveDTO>
    {
        private readonly IRepositoryTodo repositoryTodo;
        private readonly IRepositoryUser repositoryUser;
        public TodoSaveValidator(IRepositoryTodo repositoryTodo, IRepositoryUser repositoryUser)
        {
            this.repositoryTodo = repositoryTodo;
            this.repositoryUser = repositoryUser;

            RulesForId();
            RulesForCode();
            RulesForDescription();
            RulesForDate();
            RulesForUserId();
        }

        private void RulesForId()
        {
            RuleFor(x => x.Id)
                .MustAsync(async (Guid? id, CancellationToken cancellationToken) =>
                {
                    return id is null || await repositoryTodo.ExistsAsync(id.Value, cancellationToken);
                }).WithMessage("Todo não encontrado");
        }

        private void RulesForCode()
        {
            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty().WithMessage("Por favor, preencha o código")
                .MustAsync(async (model, code, cancellationToken) =>
                {
                    return model.Id is null
                        ? !await repositoryTodo.ExistsCodeAsync(code, model.UserId, cancellationToken)
                        : !await repositoryTodo.ExistsCodeExceptIdAsync(model.Id.Value, code, model.UserId, cancellationToken);
                }).WithMessage("Código já cadastrado");
        }

        private void RulesForDescription()
        {
            RuleFor(x => x.Description).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage("Por favor, preencha a descrição");
        }

        private void RulesForDate() => RuleFor(x => x.Date).NotNull().WithMessage("Por favor, preencha a data");

        private void RulesForUserId()
        {
            RuleFor(x => x.UserId)
                .MustAsync(async (Guid id, CancellationToken cancellationToken) =>
                {
                    return await repositoryUser.ExistsAsync(id, cancellationToken);
                }).WithMessage("Usuário não encontrado");
        }
    }
}
