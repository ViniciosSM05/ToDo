namespace DDDApi.Domain.Core.Interfaces.Queue
{
    public interface IQueueClient
    {
        Task ConsumeQueueAsync<T>(string hostName, string queueName, int delayInMinutes, Action<T> callback);
        Task PostOnQueueAsync<T>(string queueHost, string queueName, T message);
    }
}
