using AutoMapper;
using KuzmichInc.Microservices.PlatformService.Dtos;
using KuzmichInc.Microservices.PlatformService.Models;
using KuzmichInc.Microservices.Repositories;
using KuzmichInc.Microservices.Services;

namespace KuzmichInc.Microservices.PlatformService.Services
{
    public class PlatformBusinessService : BaseService<PlatformResponseDto, PlatformRequestDto, Platform>
    {
        public PlatformBusinessService(IRepository<Platform> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}
