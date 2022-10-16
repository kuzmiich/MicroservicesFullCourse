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
            await using var scope = _scopeFactory.CreateAsyncScope();
            var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishDto>(platformPublishedMessage);

            var platformCreateDto = _mapper.Map<PlatformCreateDto>(platformPublishedDto);
            if (!repository.ExternalPlatformExist(platformCreateDto.ExternalPlatformId))
            {
                await repository.CreatePlatform(platformCreateDto);
                await repository.SaveChanges();
            }
        }

        private enum EventType
        {
            PlatformPublished,
            Undetermined
        }
    }
}