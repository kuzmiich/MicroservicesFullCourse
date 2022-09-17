using Microservices.PlatformService.Dtos;
using System.Threading.Tasks;

namespace Microservices.CommandService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformResponseDto platformResponseDto);
    }
}
