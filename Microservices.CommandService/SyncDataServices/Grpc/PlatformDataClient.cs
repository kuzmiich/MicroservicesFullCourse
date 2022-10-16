using AutoMapper;
using Grpc.Net.Client;
using Microservices.CommandService.Models;
using Microservices.PlatformService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Microservices.CommandService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly GrpcPlatform.GrpcPlatformClient _client;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _client = InitGrpcPlatformClient();
        }

        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            PlatformResponse response;
            try
            {
                var request = new GetAllRequest();
                response = _client?.GetAllPlatforms(request);
            }
            catch (Exception e)
            {
                Console.WriteLine($"The GRPc client did not initialize. Error - {e.Message}");
                return Enumerable.Empty<Platform>();
            }
            
            return _mapper.Map<IEnumerable<Platform>>(response?.Platform) ?? Enumerable.Empty<Platform>();
        }

        private GrpcPlatform.GrpcPlatformClient InitGrpcPlatformClient()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
            
            return new GrpcPlatform.GrpcPlatformClient(channel);
        }
    }
}