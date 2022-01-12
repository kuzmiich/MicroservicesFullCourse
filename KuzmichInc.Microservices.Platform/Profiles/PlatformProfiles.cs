using AutoMapper;
using KuzmichInc.Microservices.PlatformService.Dtos;
using KuzmichInc.Microservices.PlatformService.Models;

namespace KuzmichInc.Microservices.PlatformService.Profiles
{
    public class PlatformProfiles : Profile
    {
        public PlatformProfiles()
        {
            CreateMap<PlatformResponseDto, Platform>()
                .ReverseMap();
        }
    }
}
