namespace DDDApi.Domain.Core.Interfaces.Queue
{
    public interface IQueueClient
    {
        Task ConsumeQueueAsync<T>(string hostName, string queueName, Action<T> callback, CancellationToken cancellationToken);
        Task PostOnQueueAsync<T>(string queueHost, string queueName, T message);
    }
}
