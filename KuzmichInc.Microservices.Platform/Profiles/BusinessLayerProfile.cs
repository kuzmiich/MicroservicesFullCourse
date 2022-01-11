using AutoMapper;
using KuzmichInc.Microservices.PlatformService.Dtos;
using KuzmichInc.Microservices.PlatformService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Profiles
{
    public class BusinessLayerProfile : Profile
    {
        public BusinessLayerProfile()
        {
            CreateMap<PlatformDto, PlatformViewModel>()
                .ReverseMap();
        }
    }
}
