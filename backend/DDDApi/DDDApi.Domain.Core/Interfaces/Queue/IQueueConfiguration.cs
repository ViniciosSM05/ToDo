namespace DDDApi.Domain.Core.Interfaces.Queue
{
    public interface IQueueConfiguration
    {
        string Host { get; }
        string Name { get; }
    }
}
