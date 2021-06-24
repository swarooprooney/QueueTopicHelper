using System.Threading.Tasks;

namespace QueueTopicHelper.Sender.InternalServices
{
    internal interface ISender
    {
        Task SendMessageAsync<T>(T message);
    }
}
