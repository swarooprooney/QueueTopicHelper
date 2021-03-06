using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueTopicHelper.Receiver
{
    public class ServiceBusQueueReceiver : IServiceBusQueueReceiver
    {

        private static IQueueClient queueClient;
        private readonly string _connectionString;
        public event EventHandler<string> RaiseMessageReadyEvent;
        public event EventHandler<ExceptionModel> RaiseExceptionEvent;
        public ServiceBusQueueReceiver(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            queueClient = new QueueClient(_connectionString, queueName);
        }

        public async Task ReadMessageAsync()
        {
            var messageOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(MessageHandler, messageOptions);
            await Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await queueClient.CloseAsync();
        }

        private async Task MessageHandler(Message message, CancellationToken cancellationToken)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            RaiseMessageReadyEvent?.Invoke(this, jsonString);
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            RaiseExceptionEvent?.Invoke(this, ExceptionConverter.ConvertToExceptionModel(arg));
            await Task.CompletedTask;
        }
    }
}
