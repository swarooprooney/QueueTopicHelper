using Microsoft.Extensions.DependencyInjection;
using QueueTopicHelper.Enums;
using QueueTopicHelper.Sender.InternalServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace QueueTopicHelper.Factory
{
    internal class SenderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SenderFactory( IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        internal ISender GetConcreteSender(QueueOrTopicType type)
        {
            switch (type)
            {
                case QueueOrTopicType.ServiceBusQueue:
                    return _serviceProvider.GetRequiredService<IServiceBusQueueService>();
                case QueueOrTopicType.ServiceBusTopic:
                    return _serviceProvider.GetRequiredService<IServiceBusTopicService>();
                default:
                    return null;
            }
        }
    }
}
