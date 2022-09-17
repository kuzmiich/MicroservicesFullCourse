using AutoMapper;
using Microservices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.PlatformService.Dtos;

namespace Microservices.PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IUnitOfWorkService<PlatformResponseDto, PlatformRequestDto> _service;
        private readonly IMapper _mapper;

        public PlatformController(IUnitOfWorkService<PlatformResponseDto, PlatformRequestDto> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformResponseDto>>> GetPlatforms()
        {
            var platforms = await _service.GetAllAsync();

            return Ok(platforms);
        }
        
        [HttpGet("{id:int}", Name = "Platform/")]
        public async Task<ActionResult<PlatformResponseDto>> GetPlatformById(int id)
        {
            var platform = await _service.GetByIdAsync(id);
            if (platform is null)
            {
                return NotFound();
            }

            return Ok(platform);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlatformResponseDto>> CreatePlatform(PlatformRequestDto platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var platformResponseDto = await _service.CreateAsync(platform);
            
            return CreatedAtRoute(nameof(GetPlatformById), new { platformResponseDto.Id }, platformResponseDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlatformResponseDto>> UpdatePlatform(PlatformResponseDto platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var platformResponseDto = await _service.UpdateAsync(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { platformResponseDto.Id }, platformResponseDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PlatformResponseDto>> DeletePlatform(int id)
        {
            var platformResponseDto = await _service.GetByIdAsync(id);

            await _service.DeleteAsync(id);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = id }, platformResponseDto);
        }
    }
}
