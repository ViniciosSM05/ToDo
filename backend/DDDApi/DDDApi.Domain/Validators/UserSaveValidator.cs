using DDDApi.Domain.Core.DTO.User;
using DDDApi.Domain.Core.Interfaces.Repository;
using FluentValidation;

namespace DDDApi.Domain.Validators
{
    public class UserSaveValidator : AbstractValidator<UserSaveDTO>
    {
        private readonly IRepositoryUser repositoryUser;
        public UserSaveValidator(IRepositoryUser repositoryUser)
        {
            this.repositoryUser = repositoryUser;

            RulesForName();
            RulesForEmail();
            RulesForPassword();
            RulesForPasswordConfirm();
        }

        private void RulesForName() => RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Por favor, preencha o nome");

        private void RulesForEmail()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Por favor, preencha o e-mail")
                .EmailAddress().WithMessage("Por favor, insira um e-mail válido")
                .MustAsync(async (string email, CancellationToken cancellationToken) =>
                {
                    return !await repositoryUser.ExistsEmailAsync(email, cancellationToken);
                }).WithMessage("E-mail já existente");
        }

        private void RulesForPassword()
        {
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty().WithMessage("Por favor, preencha a senha")
                .MinimumLength(8).WithMessage("A senha deve conter no mínimo 8 caracteres")
                .MaximumLength(15).WithMessage("A senha deve conter no máximo 15 caracteres");
        }

        private void RulesForPasswordConfirm() => RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("As senhas não coincidem");
    }
}
