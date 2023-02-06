using AutoMapper;
using Microservices.CommandService.Data;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.CommandService.Services
{
    public class CommandService : ICommandService
    {
        private readonly CommandContext _context;
        private readonly IMapper _mapper;

        public CommandService(CommandContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = await _context.Platforms.AsNoTracking().ToListAsync();
            
            return _mapper.Map<List<PlatformReadDto>>(platforms) ?? new List<PlatformReadDto>();
        }

        public async Task<List<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            var orderedCommands = await _context.Commands
                .Where(c => c.PlatformId.Equals(platformId))
                .OrderBy(c => c.Platform.Name)
                .ToListAsync();
            
            return _mapper.Map<List<CommandReadDto>>(orderedCommands) ?? new List<CommandReadDto>();
        }

        public async Task<CommandReadDto> GetCommand(int platformId, int commandId)
        {
            var command = await _context.Commands
                .FirstOrDefaultAsync(c => c.PlatformId.Equals(platformId) && c.Id.Equals(commandId));
            
            return _mapper.Map<CommandReadDto>(command);
        }

        public async Task<CommandReadDto> CreateCommand(int platformId, CommandCreateDto commandCreateDto)
        {
            if (commandCreateDto is null)
                throw new ArgumentNullException(nameof(commandCreateDto));
            
            var command = _mapper.Map<Command>(commandCreateDto);
            command.PlatformId = platformId;

            var createdCommand = (await _context.Commands.AddAsync(command)).Entity;
            return _mapper.Map<CommandReadDto>(createdCommand);
        }

        public async Task<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            if (platformCreateDto == null)
                throw new ArgumentNullException(nameof(platformCreateDto));

            var platform = _mapper.Map<Platform>(platformCreateDto);
            var createdPlatform = (await _context.Platforms.AddAsync(platform)).Entity;
            await SaveChanges();
            
            return _mapper.Map<PlatformReadDto>(createdPlatform);
        }
        public bool ExternalPlatformExist(int externalPlatformId) =>
            _context.Platforms.Any(p => p.ExternalPlatformId == externalPlatformId);
        
        public bool PlatformExist(int platformId) => _context.Platforms.Any(p => p.Id == platformId);
        
        private Task SaveChanges() => _context.SaveChangesAsync();
    }
}