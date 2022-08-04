using DDDApi.Application.Applications.Base;
using DDDApi.Domain.Core.Interfaces.Application;
using DDDApi.Domain.Core.Interfaces.Service;
using DDDApi.Domain.Core.DTO.User;

namespace DDDApi.Application.Applications
{
    public class ApplicationUser : ApplicationBase, IApplicationUser
    {
        private readonly IServiceUser serviceUser;
        public ApplicationUser(IServiceUser serviceUser)
        {
            this.serviceUser = serviceUser;
        }

        public async Task<UserSaveResponseDTO> Save(UserSaveDTO obj, CancellationToken cancellationToken)
        {
            using var transacao = TransactionScopeAsync();
            var response = await serviceUser.SaveAsync(obj, cancellationToken);
            transacao.Complete();

            return response;
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO obj, CancellationToken cancellationToken)
            => await serviceUser.LoginAsync(obj, cancellationToken);
    }
}
