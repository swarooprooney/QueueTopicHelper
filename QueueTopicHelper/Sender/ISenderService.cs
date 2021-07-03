using QueueTopicHelper.Enums;
using System.Threading.Tasks;

namespace QueueTopicHelper.Sender
{
    public interface ISenderService
    {
        Task SendMessageAsync<T>(T message);
    }
}
