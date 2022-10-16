using AutoMapper;
using Grpc.Core;
using Microservices.PlatformService.Models;
using Microservices.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepository<Platform> _repository;

        public GrpcPlatformService(IUnitOfWorkRepository<Platform> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var platforms = await _repository.GetAll().ToListAsync();

            return new PlatformResponse
            {
                Platform = { _mapper.Map<IEnumerable<GrpcPlatformModel>>(platforms) }
            };
        }
    }
}