using DDDApi.Application.Applications.Base;
using DDDApi.Domain.Core.Interfaces.Application;
using DDDApi.Domain.Core.Interfaces.Service;
using DDDApi.Domain.Core.DTO.User;
using DDDApi.Domain.Core.Interfaces.Email;
using DDDApi.Domain.Core.Interfaces.Queue;

namespace DDDApi.Application.Applications
{
    public class ApplicationUser : ApplicationBase, IApplicationUser
    {
        private readonly IServiceUser serviceUser;
        private readonly ISendEmailBuilder sendEmailBuilder;
        private readonly IEmailClient emailClient;

        public ApplicationUser(
            IServiceUser serviceUser, 
            ISendEmailBuilder sendEmailBuilder,
            IEmailClient emailClient)
        {
            this.serviceUser = serviceUser;
            this.sendEmailBuilder = sendEmailBuilder;
            this.emailClient = emailClient;
        }

        public async Task<UserSaveResponseDTO> Save(UserSaveDTO obj, CancellationToken cancellationToken)
        {
            using var transacao = TransactionScopeAsync();

            var response = await serviceUser.SaveAsync(obj, cancellationToken);
            if (response is null) return response;

            await AddWelcomeEmailAsync(response.Name, response.Email);
            transacao.Complete();
            
            return response;
        }

        private async Task AddWelcomeEmailAsync(string userName, string userEmail)
        {
            var emailToSend = sendEmailBuilder
                .WithSubject("SEJA BEM VINDO AO TODO!!!")
                .WithRecipient(userEmail)
                .WithBodyHTML($"<h1>Olá {userName}, seja bem-vindo!</h1>")
                .Build();
            await emailClient.PostEmailOnQueueAsync(emailToSend);
        }

        public async Task<UserLoginResponseDTO> LoginAsync(UserLoginDTO obj, CancellationToken cancellationToken)
            => await serviceUser.LoginAsync(obj, cancellationToken);
    }
}
