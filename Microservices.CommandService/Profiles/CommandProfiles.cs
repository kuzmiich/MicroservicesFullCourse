using AutoMapper;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Models;
using Microservices.PlatformService;

namespace Microservices.CommandService.Profiles
{
    public class CommandProfiles : Profile
    {
        public CommandProfiles()
        {
            CreateMap<CommandReadDto, Command>()
                .ReverseMap();
            
            CreateMap<CommandCreateDto, Command>()
                .ReverseMap();
            
            CreateMap<PlatformReadDto, Platform>()
                .ReverseMap();

            CreateMap<PlatformPublishDto, Platform>()
                .ForMember(dest => dest.ExternalPlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalPlatformId, opt => opt.MapFrom(src => src.PlatformId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }
    }
}