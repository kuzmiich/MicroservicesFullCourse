using Microservices.PlatformService.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.CommandService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;

        public HttpCommandDataClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public Task SendPlatformToCommand(PlatformResponseDto platformResponseDto)
        {
            return null;
        }
    }
}
