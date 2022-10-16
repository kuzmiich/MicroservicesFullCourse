using AutoMapper;
using Microservices.Repositories;
using Microservices.Services;
using Microservices.PlatformService.Dtos;
using Microservices.PlatformService.Models;

namespace Microservices.PlatformService.Services
{
    public class PlatformBusinessService : BaseCrudService<PlatformReadDto, PlatformCreateDto, Platform>
    {
        public PlatformBusinessService(IUnitOfWorkRepository<Platform> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}
