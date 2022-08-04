using DDDApi.Domain.Core.Interfaces.Queue;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DDDApi.Infra.Queue.Clients
{
    public class QueueClient : IQueueClient
    {
        public Task PostOnQueueAsync<T>(string queueHost, string queueName, T message)
        {
            var json = JsonConvert.SerializeObject(message);
            var factory = new ConnectionFactory() { HostName = queueHost };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
            return Task.CompletedTask;
        }

        public async Task ConsumeQueueAsync<T>(string hostName, string queueName, int delayInMinutes, Action<T> callback)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var stringMessage = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<T>(stringMessage);
                callback(message);
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            while (true) await Task.Delay(delayInMinutes * 1000 * 60);
        }
    }
}
