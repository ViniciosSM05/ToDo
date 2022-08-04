using DDDApi.Domain.Core.Interfaces.Application;

namespace DDDApi.WebApplication.BackgroundServices
{
    public class CheckNextTodosBackgroundService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private Timer? _timer = null;

        public CheckNextTodosBackgroundService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            using var scope = serviceProvider.CreateScope();
            var applicationTodo = scope.ServiceProvider.GetService<IApplicationTodo>();
            applicationTodo.CheckNextTodosAsync(CancellationToken.None).Wait();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
