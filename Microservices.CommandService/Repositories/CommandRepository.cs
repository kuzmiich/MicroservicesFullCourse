using System;
using System.Linq;
using System.Threading.Tasks;
using Microservices.CommandService.Data;
using Microservices.CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.CommandService.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        private readonly CommandContext _context;

        public CommandRepository(CommandContext context)
        {
            _context = context;
        }

        public Task CreateCommand(int platformId, Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            command.PlatformId = platformId;
            return _context.Commands.AddAsync(command).AsTask();
        }

        public Task CreatePlatform(Platform plat)
        {
            if (plat == null)
                throw new ArgumentNullException(nameof(plat));

            return _context.Platforms.AddAsync(plat).AsTask();
        }

        public bool ExternalPlatformExist(int externalPlatformId) =>
            _context.Platforms.Any(p => p.ExternalPlatformId == externalPlatformId);

        public IQueryable<Platform> GetAllPlatforms() => _context.Platforms.AsNoTracking();

        public Task<Command> GetCommand(int platformId, int commandId)
        {
            return _context.Commands
                .FirstOrDefaultAsync(c => c.PlatformId == platformId && c.Id == commandId);
        }

        public IOrderedQueryable<Command> GetCommandsForPlatform(int platformId) =>
            _context.Commands
                .AsQueryable()
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);

        public bool PlatformExist(int platformId) => _context.Platforms.Any(p => p.Id == platformId);

        public Task SaveChanges() => _context.SaveChangesAsync();
    }
}