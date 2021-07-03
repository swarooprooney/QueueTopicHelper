using System.Threading.Tasks;

namespace QueueTopicHelper.Receiver
{
    public interface IStorageQueueReceiver
    {
        Task DeleteMessageFromQueue(string messageId, string popReceipt);
        Task<string> ReadMessagesAsync(int numberOfMessages);
    }
}