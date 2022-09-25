using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Models;
using Microservices.CommandService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Microservices.CommandService.Controllers
{
    [Route("api/c/platform/{platformId}/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;

        public CommandController(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            if (!_repository.PlatformExist(platformId))
                return NotFound();

            var platform = await _repository.GetCommandsForPlatform(platformId).ToListAsync();
            return Ok(_mapper.Map<List<CommandReadDto>>(platform));
        }

        [HttpGet("{commandId:int}", Name = "GetCommandForPlatform")]
        public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");

            if (!_repository.PlatformExist(platformId))
                return NotFound();

            var command = await _repository.GetCommand(platformId, commandId);

            if (command == null)
                return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            if (!_repository.PlatformExist(platformId))
                return NotFound();

            var command = _mapper.Map<Command>(commandCreateDto);
            await _repository.CreateCommand(platformId, command);
            await _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetCommandForPlatform), 
                new { platformId, commandId = command.Id },
                _mapper.Map<CommandReadDto>(command));
        }
    }
}