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
        }
    }
}
