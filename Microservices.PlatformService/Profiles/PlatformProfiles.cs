using AutoMapper;
using Microservices.PlatformService.Dtos;
using Microservices.PlatformService.Models;

namespace Microservices.PlatformService.Profiles
{
    public class PlatformProfiles : Profile
    {
        public PlatformProfiles()
        {
            CreateMap<PlatformResponseDto, Platform>()
                .ReverseMap();

            CreateMap<PlatformRequestDto, Platform>()
                .ReverseMap();
        }
    }
}
