using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QueueTopicHelper.Receiver
{
    public class StorageQueueReceiver : IStorageQueueReceiver
    {
        private static QueueClient client;

        public StorageQueueReceiver(string connectionString, string queueName)
        {
            client = new QueueClient(connectionString, queueName);
        }

        public async Task<string> ReadMessagesAsync(int numberOfMessages)
        {
            if (await client.ExistsAsync())
            {
                QueueMessage[] retrievedMessage = await client.ReceiveMessagesAsync(numberOfMessages);
                return JsonSerializer.Serialize(retrievedMessage);
            }
            throw new Exception("the queue does not exist");
        }

        public async Task DeleteMessageFromQueue(string messageId,string popReceipt)
        {
            await client.DeleteMessageAsync(messageId, popReceipt);
        }

    }
}
