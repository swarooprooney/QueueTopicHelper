using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QueueTopicHelper.Sender.InternalServices
{
    internal class ServiceBusQueueService : IServiceBusQueueService
    {
        private static IQueueClient client;

        public ServiceBusQueueService(string connectionString, string queueName)
        {
            client = new QueueClient(connectionString, queueName);
        }

        async Task ISender.SendMessageAsync<T>(T serviceBusMessage)
        {
            var messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await client.SendAsync(message);
        }

    }
}
