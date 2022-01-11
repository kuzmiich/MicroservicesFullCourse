using KuzmichInc.Microservices.PlatformService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Data
{
    public static class PlatformContextInitialization
    {
        public static async Task InitContextAsync(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            
            await SeedData(serviceScope.ServiceProvider.GetService<PlatformContext>());
        }

        private static async Task SeedData(PlatformContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");

                await context.Platforms.AddRangeAsync(
                    new Platform() { Name = ".Net", Publisher="Microsoft", Cost = "Free"},
                    new Platform() { Name = "Sql Server Express", Publisher="Microsoft", Cost = "Free"},
                    new Platform() { Name = "Kubernetes", Publisher="Cloud Native Computing Foundation", Cost = "Free"}
                );

            }
            Console.WriteLine("--> We already have data");
        }
    }
}
