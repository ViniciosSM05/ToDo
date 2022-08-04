namespace DDDApi.Domain.Core.Interfaces.Queue
{
    public interface IQueueClient
    {
        void ConsumeQueue<T>(string hostName, string queueName, Action<T> callback);
        Task PostOnQueueAsync<T>(string queueHost, string queueName, T message);
    }
}
