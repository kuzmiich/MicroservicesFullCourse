using AutoMapper;
using KuzmichInc.Microservices.PlatformService.Dtos;
using KuzmichInc.Microservices.PlatformService.Models;

namespace KuzmichInc.Microservices.PlatformService.Profiles
{
    public class DataAccessProfile : Profile
    {
        public DataAccessProfile()
        {
            CreateMap<Platform, PlatformDto>()
                .ReverseMap();
        }
    }
}
