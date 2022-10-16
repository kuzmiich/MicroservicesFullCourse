using Microservices.PlatformService.Dtos;

namespace Microservices.PlatformService.SyncDataServices.RabbitMQ
{
    public interface IMessageBusClient
    {
        void PublishPlatform(PlatformPublishDto platformPublishedDto);
    }
}