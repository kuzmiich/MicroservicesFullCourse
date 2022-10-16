using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using Microservices.CommandService.Models;
using Microservices.PlatformService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

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
            var request = new GetAllRequest();
            var response = _client?.GetAllPlatforms(request);
            return _mapper.Map<IEnumerable<Platform>>(response?.Platform);
        }

        private GrpcPlatform.GrpcPlatformClient InitGrpcPlatformClient()
        {
            Console.WriteLine($"--> Calling GRPC Service {_configuration["GrpcPlatform"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
            return channel.State == ConnectivityState.Idle ? 
                new GrpcPlatform.GrpcPlatformClient(channel) : 
                null;
        }
    }
}