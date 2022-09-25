using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microservices.PlatformService.Dtos;
using Microsoft.Extensions.Configuration;

namespace Microservices.PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        
        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        { 
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platformReadDto),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(_configuration["CommandServiceURL"], httpContent);

            Console.WriteLine(response.IsSuccessStatusCode
                ? "--> Sync POST to CommandService was OK!"
                : "--> Sync POST to CommandService was NOT OK!");
        }
    }
}
