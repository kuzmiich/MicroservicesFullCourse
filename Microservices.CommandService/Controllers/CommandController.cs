using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Microservices.CommandService.Controllers
{
    [Route("api/c/platform/{platformId:int}/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _service;
        private readonly IMapper _mapper;

        public CommandController(ICommandService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommandReadDto>>> GetCommandsForPlatform(int platformId)
        {
            if (!_service.PlatformExist(platformId))
                return NotFound($"Platform with this id:'{platformId}', doesn't exist in the Database.");

            var commands = await _service.GetCommandsForPlatform(platformId);
            
            return Ok(_mapper.Map<List<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId:int}", Name = "GetCommandForPlatform")]
        public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            if (!_service.PlatformExist(platformId))
                return NotFound($"Platform with this id:'{platformId}', doesn't exist in the Database.");

            var command = await _service.GetCommand(platformId, commandId);

            if (command is null)
                return NotFound($"Commands with this id:'{commandId}', doesn't exist in the Database.");

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommandForPlatform(int platformId,
            CommandCreateDto commandCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_service.PlatformExist(platformId))
                return NotFound($"Platform with this id:'{platformId}', doesn't exist in the Database");

            var commandReadDto = await _service.CreateCommand(platformId, commandCreateDto);

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new { platformId, commandId = commandReadDto.Id },
                commandReadDto);
        }
    }
}