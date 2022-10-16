using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Models;
using Microservices.CommandService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Microservices.CommandService.Controllers
{
    [Route("api/c/platform/{platformId:int}/[controller]")]
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<CommandReadDto>>> GetCommandsForPlatform(int platformId)
        {
            if (!_repository.PlatformExist(platformId))
                return NotFound($"Platform with this id:'{platformId}', doesn't exist in the Database.");

            var commands = await _repository.GetCommandsForPlatform(platformId).ToListAsync();
            return Ok(_mapper.Map<List<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId:int}", Name = "GetCommandForPlatform")]
        public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            if (!_repository.PlatformExist(platformId))
                return NotFound($"Platform with this id:'{platformId}', doesn't exist in the Database.");

            var command = await _repository.GetCommand(platformId, commandId);

            if (command == null)
                return NotFound($"Commands with this id:'{commandId}', doesn't exist in the Database.");

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommandForPlatform(int platformId,
            CommandCreateDto commandCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value.Errors)
                    .Where(y=>y.Count>0)
                    .ToList());

            if (!_repository.PlatformExist(platformId))
                return NotFound($"Platform with this id:'{platformId}', doesn't exist in the Database");

            var command = _mapper.Map<Command>(commandCreateDto);
            await _repository.CreateCommand(platformId, command);
            await _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new { platformId, commandId = command.Id },
                _mapper.Map<CommandReadDto>(command));
        }
    }
}