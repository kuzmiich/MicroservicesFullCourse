using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microservices.PlatformService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Microservices.PlatformService.Data
{
    public static class PlatformsContextInitialization
    {
        public static async Task InitContextAsync(this IHost app)
        {
            await using var serviceScope = app.Services.CreateAsyncScope();
            var environment = serviceScope.ServiceProvider.GetService<IWebHostEnvironment>();
            var context = serviceScope.ServiceProvider.GetService<PlatformsContext>();
            
            await SeedPlatformAsync(context, environment.IsProduction());
        }

        private static async Task SeedPlatformAsync(PlatformsContext context, bool isProd)
        {
            var isMigrated = (await context.Database.GetPendingMigrationsAsync()).Any();
            if (!isProd && !isMigrated)
            {
                try
                {
                    await context.Database.MigrateAsync();
                    Console.WriteLine("Data schema was successfully updated.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations: {ex.Message}");
                }
            }
            
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");

                await context.Platforms.AddRangeAsync(
                    new Platform() { Name = ".Net", Publisher="Microsoft", Cost = "Free"},
                    new Platform() { Name = "Sql Server Express", Publisher="Microsoft", Cost = "Free"},
                    new Platform() { Name = "Kubernetes", Publisher="Cloud Native Computing Foundation", Cost = "Free"}
                );
                await context.SaveChangesAsync();
                return;
            }
            
            Console.WriteLine("--> We already have data in the server!");
        }
    }
}
