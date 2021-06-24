using QueueTopicHelper.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueueTopicHelper.Sender
{
    public interface ISenderService
    {
        Task SendMessageAsync<T>(T message, QueueOrTopicType type);
    }
}
