using AutoMapper;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices.CommandService.SyncDataServices.RabbitMQ.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public async Task ProcessEvent(string message)
        {
            switch (DetermineEvent(message))
            {
                case EventType.PlatformPublished:
                    await AddPlatform(message);
                    break;
                case EventType.Undetermined:
                default:
                    throw new NotSupportedException("This type of event is didn't supported.");
            }
        }


        private EventType DetermineEvent(string notificationMessage) =>
            JsonSerializer.Deserialize<GenericEventDto>(notificationMessage)?.Event switch
            {
                "Platform_Published" => EventType.PlatformPublished,
                _ => EventType.Undetermined
            };

        private async Task AddPlatform(string platformPublishedMessage)
        {
            if (string.IsNullOrEmpty(platformPublishedMessage)) return;
            
            await using var scope = _scopeFactory.CreateAsyncScope();
            var service = scope.ServiceProvider.GetRequiredService<ICommandService>();
            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishDto>(platformPublishedMessage);

            if (platformPublishedDto is null) return; 
            
            var platformCreateDto = _mapper.Map<PlatformCreateDto>(platformPublishedDto);
            if (!service.ExternalPlatformExist(platformCreateDto.ExternalPlatformId))
            {
                await service.CreatePlatform(platformCreateDto);
            }
        }

        private enum EventType
        {
            PlatformPublished,
            Undetermined
        }
    }
}