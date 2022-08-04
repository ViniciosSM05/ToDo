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
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            using var scope = serviceProvider.CreateScope();
            var emailClient = scope.ServiceProvider.GetService<IEmailClient>();
            emailClient.ConsumeEmailsByQueue();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;               
        }
    }
}
