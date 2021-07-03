using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QueueTopicHelper.Sender.InternalServices
{
    internal class StorageQueueService : IStorageQueueService
    {
        private static QueueClient client;

        public StorageQueueService(string connectionString, string queueName)
        {
            client = new QueueClient(connectionString, queueName);
        }
        public async Task SendMessageAsync<T>(T message)
        {
            await client.CreateIfNotExistsAsync();
            if (await client.ExistsAsync())
            {
                var messageAsJson = JsonSerializer.Serialize(message);
                await client.SendMessageAsync(messageAsJson);
            }
            else
            {
                throw new Exception("Unable to create the queue");
            }
        }
    }
}
