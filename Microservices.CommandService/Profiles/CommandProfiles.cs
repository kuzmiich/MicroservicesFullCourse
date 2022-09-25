using AutoMapper;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Models;

namespace Microservices.CommandService.Profiles
{
    public class CommandProfiles : Profile
    {
        public CommandProfiles()
        {
            CreateMap<CommandReadDto, Command>()
                .ReverseMap();
            
            CreateMap<Dtos.CommandCreateDto, Command>()
                .ReverseMap();
            
            CreateMap<PlatformReadDto, Platform>()
                .ReverseMap();
        }
    }
}