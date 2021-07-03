using Microsoft.Extensions.DependencyInjection;
using QueueTopicHelper.Enums;
using QueueTopicHelper.Sender.InternalServices;
using System.Threading.Tasks;

namespace QueueTopicHelper.Sender
{
    public class SenderService : ISenderService
    {
        private readonly IServiceCollection _serviceCollection;

        public SenderService(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public async Task SendMessageAsync<T>(T message)
        {
            await _serviceCollection.BuildServiceProvider().GetRequiredService<ISender>().SendMessageAsync(message);
        }
    }
}
