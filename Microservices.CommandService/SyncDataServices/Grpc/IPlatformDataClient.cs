using Microservices.CommandService.Models;
using System.Collections.Generic;

namespace Microservices.CommandService.SyncDataServices.Grpc
{
    public interface IPlatformDataClient
    {
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}