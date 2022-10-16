using Microservices.PlatformService.Dtos;

namespace Microservices.PlatformService.AsyncDataService.RabbitMQ
{
    public interface IMessageBusClient
    {
        void PublishPlatform(PlatformPublishDto platformPublishedDto);
    }
}