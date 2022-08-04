using DDDApi.Domain.Core.DTO.Email;

namespace DDDApi.Domain.Core.Interfaces.Email
{
    public interface IEmailClient
    {
        Task SendEmailAsync(SendEmailDTO email, CancellationToken cancellationToken);
        void ConsumeEmailsByQueue();
        Task PostEmailOnQueueAsync(SendEmailDTO sendEmail);
    }
}
