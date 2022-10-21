using Microservices.CommandService.Dtos;
using Microservices.CommandService.Models;
using Microservices.CommandService.Services;
using Microservices.CommandService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.CommandService.Data
{
    public static class CommandContextInitialization
    {
        public static async Task InitContextAsync(this IHost app)
        {
            await using var serviceScope = app.Services.CreateAsyncScope();
            var environment = serviceScope.ServiceProvider.GetService<IWebHostEnvironment>();
            var context = serviceScope.ServiceProvider.GetService<CommandContext>();
            
            await SeedCommandContext(context, environment.IsProduction());
            
            var repository = serviceScope.ServiceProvider.GetService<ICommandService>();
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
            var platforms = grpcClient?.ReturnAllPlatforms();
            
            await SeedPlatformFromExternalService(repository, platforms);
        }

        private static async Task SeedCommandContext(CommandContext context, bool isProd)
        {
            if (isProd)
            {
                try
                {
                    await context.Database.MigrateAsync();
                    Console.WriteLine("Data was successfully migrated.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations: {ex.Message}");
                }
            }

            if (context.Platforms.Any() && context.Commands.Any())
            {
                Console.WriteLine("--> We already have data");
                return;
            }
            
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data for platforms...");

                await context.Platforms.AddRangeAsync(
                    new Platform() { ExternalPlatformId = 1, Name = "Platform 1"},
                    new Platform() { ExternalPlatformId = 2, Name = "Platform 2"},
                    new Platform() { ExternalPlatformId = 3, Name = "Platform 3"}
                );
                await context.SaveChangesAsync();
            }

            if (!context.Commands.Any())
            {
                Console.WriteLine("--> Seeding data for commands...");
                
                await context.Commands.AddRangeAsync(
                    new Command { PlatformId = 1, HowToDoActivity= "Build image", CommandLine = "docker build -t (image_id/image_name) -f (Dockerfile_Name) ." },
                    new Command { PlatformId = 2, HowToDoActivity = "Create container", CommandLine = "docker run -p 8080:80 (image_id/image_name)"},
                    new Command { PlatformId = 3, HowToDoActivity = "Push to https://hub.docker.com/", CommandLine = "docker push (image_id/image_name)"}
                );
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedPlatformFromExternalService(ICommandService service, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("--> Seeding new platforms from external service...");

            foreach (var platform in platforms)
            {
                if(!service.ExternalPlatformExist(platform.ExternalPlatformId))
                {
                    var platformCreateDto = new PlatformCreateDto()
                    {
                        ExternalPlatformId = platform.ExternalPlatformId,
                        Name = platform.Name
                    };
                    await service.CreatePlatform(platformCreateDto);
                }
            }
        }
    }
}