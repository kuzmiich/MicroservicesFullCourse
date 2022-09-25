using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Models;

namespace Microservices.CommandService.Repositories
{
    public interface ICommandRepository
    {
        // Platforms
        IQueryable<Platform> GetAllPlatforms();
        Task CreatePlatform(Platform platform);
        bool PlatformExist(int platformId);
        bool ExternalPlatformExist(int externalPlatformId);

        // Commands
        IOrderedQueryable<Command> GetCommandsForPlatform(int platformId);
        Task<Command> GetCommand(int platformId, int commandId);
        Task CreateCommand(int platformId, Command command);
        
        Task SaveChanges();
    }
}