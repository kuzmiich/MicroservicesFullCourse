using AutoMapper;
using Microservices.PlatformService.Dtos;
using Microservices.PlatformService.Models;

namespace Microservices.PlatformService.Profiles
{
    public class PlatformProfiles : Profile
    {
        public PlatformProfiles()
        {
            CreateMap<PlatformReadDto, Platform>()
                .ReverseMap();

            CreateMap<PlatformCreateDto, Platform>()
                .ReverseMap();

            CreateMap<PlatformReadDto, PlatformPublishDto>()
                .ReverseMap();

            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));
        }
    }
}