using System.Threading.Tasks;
using Microservices.PlatformService.Dtos;

namespace Microservices.PlatformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto platformReadDto);
    }
}
