using Microservices.CommandService.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.CommandService.Services
{
    public interface ICommandRepository
    {
        // Platforms
        Task<List<PlatformReadDto>> GetAllPlatforms();
        Task CreatePlatform(PlatformCreateDto platformCreateDto);
        bool PlatformExist(int platformId);
        bool ExternalPlatformExist(int externalPlatformId);

        // Commands
        Task<List<CommandReadDto>> GetCommandsForPlatform(int platformId);
        Task<CommandReadDto> GetCommand(int platformId, int commandId);
        Task<CommandReadDto> CreateCommand(int platformId, CommandCreateDto command);
        
        Task SaveChanges();
    }
}