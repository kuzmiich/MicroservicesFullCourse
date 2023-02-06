using System;
using AutoMapper;
using Microservices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.PlatformService.Dtos;
using Microservices.PlatformService.SyncDataServices.Http;
using Microservices.PlatformService.SyncDataServices.RabbitMQ;

namespace Microservices.PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IBaseService<PlatformReadDto, PlatformCreateDto> _service;
        private readonly IMapper _mapper;

        public PlatformController(IBaseService<PlatformReadDto, PlatformCreateDto> service,
            ICommandDataClient commandDataClient, IMessageBusClient messageBusClient, IMapper mapper)
        {
            _service = service;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
        {
            var platforms = await _service.GetAllAsync();

            return Ok(platforms);
        }

        [HttpGet("{id:int}", Name = "GetPlatformById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
        {
            PlatformReadDto platform;
            try
            {
                platform = await _service.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                return e switch
                {
                    ArgumentNullException => NotFound(
                        $"Exception type - {nameof(ArgumentNullException)} \nException message - {e.Message}"),
                    ArgumentException => NotFound(
                        $"Exception type - {nameof(ArgumentException)} \nException message - {e.Message}"),
                    _ => BadRequest(
                        $"Exception type - {nameof(BadHttpRequestException)} \nException message - {e.Message}")
                };
            }

            return Ok(platform);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platform)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var platformReadDto = await _service.CreateAsync(platform);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send by HttpClient: {ex.Message}");
            }

            // Send async message to Message Bus
            try
            {
                var platformPublishDto = _mapper.Map<PlatformPublishDto>(platformReadDto);
                platformPublishDto.Event = "Platform_Published";
                _messageBusClient.PublishPlatform(platformPublishDto);
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not send by RabbitMq: {e.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new { platformReadDto.Id }, platformReadDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlatformReadDto>> UpdatePlatform(PlatformReadDto platform)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            PlatformReadDto platformReadDto;
            try
            {
                platformReadDto = await _service.UpdateAsync(platform);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(platformReadDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlatformReadDto>> DeletePlatform(int id)
        {
            PlatformReadDto platformReadDto;
            try
            {
                platformReadDto = await _service.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                return e switch
                {
                    ArgumentNullException => NotFound(
                        $"Exception type - {nameof(ArgumentNullException)} \nException message - {e.Message}"),
                    ArgumentException => NotFound(
                        $"Exception type - {nameof(ArgumentException)} \nException message - {e.Message}"),
                    _ => BadRequest($"Exception type - {nameof(Exception)} \nException message - {e.Message}")
                };
            }

            await _service.DeleteAsync(id);

            return Ok(platformReadDto);
        }
    }
}