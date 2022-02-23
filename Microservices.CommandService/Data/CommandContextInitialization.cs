using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Microservices.CommandService.Data
{
    public static class CommandContextInitialization
    {
        public static async Task InitContextAsync(this IHost app)
        {
            await using var serviceScope = app.Services.CreateAsyncScope();

            var context = serviceScope.ServiceProvider.GetService<PlatformContext>();
            await SeedPlatformAsync(context);
            await context.SaveChangesAsync();
        }

        private static Task SeedPlatformAsync(PlatformContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding data...");

                return context.Platforms.AddRangeAsync(
                    new Platform() { Name = ".Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );
            }

            Console.WriteLine("--> We already have data");
            return Task.FromResult(false);
        }
    }
}
