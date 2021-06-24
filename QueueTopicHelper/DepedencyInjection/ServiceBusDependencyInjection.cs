using Microsoft.Extensions.DependencyInjection;
using QueueTopicHelper.Enums;
using QueueTopicHelper.Receiver;
using QueueTopicHelper.Sender;
using QueueTopicHelper.Sender.InternalServices;

namespace QueueTopicHelper.DependencyInjection
{
    public static class ServiceBusDependencyInjection
    {
        public static IServiceCollection RegisterSenderForServiceBusQueue(this IServiceCollection services, string connectionString, string queueName)
        {
            services.AddSingleton<IServiceBusQueueService>(s => new ServiceBusQueueService(connectionString, queueName));
            services.AddSingleton<ISenderService>(p => new SenderService(services));
            return services;
        }
        public static IServiceCollection RegisterSender(this IServiceCollection services, string connectionString, string queueName,QueueOrTopicType type)
        {
            switch (type)
            {
                case QueueOrTopicType.ServiceBusQueue:
                    services.AddSingleton<IServiceBusQueueService>(s => new ServiceBusQueueService(connectionString, queueName));
                    break;
                case QueueOrTopicType.ServiceBusTopic:
                    services.AddSingleton<IServiceBusTopicService>(s => new ServiceBusTopicService(connectionString, queueName));
                    break;
                default:
                    services.AddSingleton<IServiceBusQueueService>(s => new ServiceBusQueueService(connectionString, queueName));
                    break;
            }
            services.AddSingleton<ISenderService>(p => new SenderService(services));
            return services;
        }
        public static IServiceCollection RegisterSenderForServiceBusTopic(this IServiceCollection services, string connectionString, string queueName)
        {
            services.AddSingleton<IServiceBusTopicService>(s => new ServiceBusTopicService(connectionString, queueName));
            services.AddSingleton<ISenderService>(p => new SenderService(services));
            return services;
        }
        public static IServiceCollection RegisterRecieverForQueue(this IServiceCollection services, string connectionString, string queueName)
        {
            services.AddSingleton<IServiceBusQueueReceiver>(r => new ServiceBusQueueReceiver(connectionString, queueName));
            return services;
        }

        public static IServiceCollection RegisterRecieverForTopic(this IServiceCollection services, string connectionString, string topicName, string subscriptionName)
        {
            services.AddSingleton<IServiceBusTopicReceiver>(r => new ServiceBusTopicReceiver(connectionString, topicName, subscriptionName));
            return services;
        }
    }
}
