using Microservices.PlatformService.Dtos;

namespace Microservices.PlatformService.AsyncDataService
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishDto platformPublishedDto);
    }
}