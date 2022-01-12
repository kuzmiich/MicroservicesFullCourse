using KuzmichInc.Microservices.PlatformService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Data
{
    public static class PlatformContextInitialization
    {
        public static async Task InitContextAsync(this IHost app)
        {
            await using var serviceScope = app.Services.CreateAsyncScope();

            var context = serviceScope.ServiceProvider.GetService<PlatformContext>();
            await SeedDataAsync(context);
        }

        private static Task SeedDataAsync(PlatformContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");

                return context.Platforms.AddRangeAsync(
                    new Platform() { Name = ".Net", Publisher="Microsoft", Cost = "Free"},
                    new Platform() { Name = "Sql Server Express", Publisher="Microsoft", Cost = "Free"},
                    new Platform() { Name = "Kubernetes", Publisher="Cloud Native Computing Foundation", Cost = "Free"}
                );
            }
            
            return Task.Run(() => Console.WriteLine("--> We already have data"));
        }
    }
}
