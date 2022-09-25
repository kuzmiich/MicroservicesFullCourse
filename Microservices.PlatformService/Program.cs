using Microservices.PlatformService.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Microservices.PlatformService
{
    public class Program
    {
        private static IWebHostEnvironment _env;

        public Program(IWebHostEnvironment env)
        {
            _env = env;
        }

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.InitContextAsync();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
