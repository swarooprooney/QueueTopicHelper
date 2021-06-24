using Microsoft.Extensions.DependencyInjection;
using QueueTopicHelper.Enums;
using QueueTopicHelper.Sender.InternalServices;

namespace QueueTopicHelper.Register
{
    internal static class RegistrationProvider
    {
        internal static IServiceCollection RegisterSender(IServiceCollection services, string connectionString, string queueName, QueueOrTopicType type)
        {
            switch (type)
            {
                case QueueOrTopicType.ServiceBusQueue:
                    services.AddSingleton<ISender>(s => new ServiceBusQueueService(connectionString, queueName));
                    break;
                case QueueOrTopicType.ServiceBusTopic:
                    services.AddSingleton<ISender>(s => new ServiceBusTopicService(connectionString, queueName));
                    break;
                default:
                    services.AddSingleton<ISender>(s => new ServiceBusQueueService(connectionString, queueName));
                    break;
            }
            return services;
        }
    }
}
