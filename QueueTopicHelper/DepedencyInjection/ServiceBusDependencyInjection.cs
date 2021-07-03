using Microsoft.Extensions.DependencyInjection;
using QueueTopicHelper.Enums;
using QueueTopicHelper.Receiver;
using QueueTopicHelper.Register;
using QueueTopicHelper.Sender;

namespace QueueTopicHelper.DependencyInjection
{
    public static class ServiceBusDependencyInjection
    {
        public static IServiceCollection RegisterSender(this IServiceCollection services, string connectionString, string queueName,QueueOrTopicType type)
        {
            services = RegistrationProvider.RegisterSender(services, connectionString, queueName, type);
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

        public static IServiceCollection RegisterRecieverForStorageQueue(this IServiceCollection services, string connectionString, string queueName)
        {
            services.AddSingleton<IStorageQueueReceiver>(r => new StorageQueueReceiver(connectionString, queueName));
            return services;
        }
    }
}
