using DDDApi.Domain.Core.DTO.Email;

namespace DDDApi.Domain.Core.Interfaces.Email
{
    public interface IEmailClient
    {
        Task SendEmailAsync(SendEmailDTO email, CancellationToken cancellationToken);
        Task ConsumeEmailsByQueueAsync(CancellationToken cancellationToken);
        Task PostEmailOnQueueAsync(SendEmailDTO sendEmail);
    }
}
