using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QueueTopicHelper.Sender.InternalServices
{
    internal class ServiceBusTopicService : IServiceBusTopicService
    {
        internal static ITopicClient client;
        public ServiceBusTopicService(string connectionString, string topicName)
        {
            client = new TopicClient(connectionString, topicName);
        }

        async Task ISender.SendMessageAsync<T>(T serviceBusMessage)
        {
            var messageBody = JsonSerializer.Serialize(serviceBusMessage);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            message.UserProperties["messageType"] = typeof(T).Name;
            await client.SendAsync(message);
        }

    }
}
