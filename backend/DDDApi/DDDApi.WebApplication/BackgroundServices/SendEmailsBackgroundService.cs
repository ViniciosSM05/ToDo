using DDDApi.Domain.Core.Interfaces.Email;

namespace DDDApi.WebApplication.BackgroundServices
{
    public class SendEmailsBackgroundService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private Timer? _timer = null;
        public SendEmailsBackgroundService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.MaxValue);
            DoWork(cancellationToken);
            return Task.CompletedTask;
        }

        private void DoWork(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                using var scope = serviceProvider.CreateScope();
                var emailClient = scope.ServiceProvider.GetService<IEmailClient>();
                await emailClient.ConsumeEmailsByQueueAsync(cancellationToken);
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;               
        }
    }
}
