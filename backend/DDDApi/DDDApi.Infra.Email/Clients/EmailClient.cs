using DDDApi.Domain.Core.DTO.Email;
using DDDApi.Domain.Core.Interfaces.Email;
using DDDApi.Domain.Core.Interfaces.Queue;
using MailKit.Net.Smtp;
using MimeKit;
using System.Security.Authentication;

namespace DDDApi.Infra.Email.Clients
{
    public class EmailClient : IEmailClient
    {
        private readonly IEmailConfiguration configuration;
        private readonly IQueueClient queueClient;
        private readonly IEmailQueueConfiguration emailQueueConfiguration;
        public EmailClient(IEmailConfiguration configuration, IQueueClient queueClient, IEmailQueueConfiguration emailQueueConfiguration)
        {
            this.configuration = configuration;
            this.queueClient = queueClient;
            this.emailQueueConfiguration = emailQueueConfiguration;
        }

        public async Task SendEmailAsync(SendEmailDTO email, CancellationToken cancellationToken)
        {
            var mensagem = new MimeMessage();

            mensagem.From.Add(new MailboxAddress(configuration.Name, configuration.Address));
            mensagem.To.Add(MailboxAddress.Parse(email.Recipient));
            mensagem.Subject = email.Subject;

            var builder = new BodyBuilder { TextBody = email.BodyText ?? "", HtmlBody = email.BodyHTML ?? "" };
            mensagem.Body = builder.ToMessageBody();

            var smtp = new SmtpClient
            {
                CheckCertificateRevocation = false,
                SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13,
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };

            await smtp.ConnectAsync(configuration.SMTP, configuration.Port, configuration.Ssl, cancellationToken);

            await smtp.AuthenticateAsync(configuration.Address, configuration.Password, cancellationToken);
            await smtp.SendAsync(mensagem, cancellationToken);

            await smtp.DisconnectAsync(true, cancellationToken);
        }

        public void ConsumeEmailsByQueue()
        {
            queueClient.ConsumeQueue(
                    emailQueueConfiguration.Host, emailQueueConfiguration.Name, 
                    async (SendEmailDTO email) => await SendEmailAsync(email, CancellationToken.None));
        }

        public async Task PostEmailOnQueueAsync(SendEmailDTO sendEmail)
            => await queueClient.PostOnQueueAsync(emailQueueConfiguration.Host, emailQueueConfiguration.Name, sendEmail);
    }
}
