using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueTopicHelper.Receiver
{
    public class ServiceBusTopicReceiver : IServiceBusTopicReceiver
    {

        public event EventHandler<string> RaiseMessageReadyEvent;
        public event EventHandler<ExceptionModel> RaiseExceptionEvent;
        private static ISubscriptionClient _subscriptionClient;
        public ServiceBusTopicReceiver(string connectionString, string topicName, string subscriptionName)
        {
            _subscriptionClient = new SubscriptionClient(connectionString, topicName, subscriptionName);
        }

        public async Task CloseQueueAsync()
        {
            await _subscriptionClient.CloseAsync();
        }

        public async Task ReadMessageAsync()
        {
            var messageOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _subscriptionClient.RegisterMessageHandler(MessageHandler, messageOptions);
            await Task.CompletedTask;
        }

        private async Task MessageHandler(Message message, CancellationToken cancellationToken)
        {
            var jsonString = Encoding.UTF8.GetString(message.Body);
            RaiseMessageReadyEvent?.Invoke(this, jsonString);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            RaiseExceptionEvent?.Invoke(this, ExceptionConverter.ConvertToExceptionModel(arg));
            await Task.CompletedTask;
        }
    }
}
