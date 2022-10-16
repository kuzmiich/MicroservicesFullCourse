using AutoMapper;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Models;
using Microservices.CommandService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices.CommandService.EventProcessing
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

        public async void ProcessEvent(string message)
        {
            switch(DetermineEvent(message)) 
            {
                case EventType.PlatformPublished:
                    await AddPlatform(message);
                    break;
                case EventType.Undetermined:
                default:
                    throw new NotSupportedException("This type of event is didn't allowed");
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

            var platform = _mapper.Map<Platform>(platformPublishedDto);
            platform.Id = default;
            if (!repository.ExternalPlatformExist(platform.ExternalPlatformId))
            {
                await repository.CreatePlatform(platform);
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