using AutoMapper;
using KuzmichInc.Microservices.PlatformService.Dtos;
using KuzmichInc.Microservices.PlatformService.Models;
using KuzmichInc.Microservices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IDtoService<PlatformResponseDto, PlatformRequestDto> _service;
        private readonly IMapper _mapper;

        public PlatformController(IDtoService<PlatformResponseDto, PlatformRequestDto> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformResponseDto>>> GetPlatformsAsync()
        {
            var platforms = await _service.GetAll();

            return Ok(platforms);
        }

        [HttpGet("/{id}", Name = "GetPlatformByIdAsync")]
        public async Task<ActionResult<PlatformResponseDto>> GetPlatformByIdAsync(int id)
        {
            var platform = await _service.GetById(id);
            if (platform is null)
            {
                return NotFound();
            }

            return Ok(platform);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlatformResponseDto>> CreatePlatformAsync(PlatformRequestDto platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var platformResponseDto = await _service.Create(platform);
            
            return CreatedAtRoute(nameof(GetPlatformByIdAsync), new { platformResponseDto.Id }, platformResponseDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlatformResponseDto>> UpdatePlatformAsync(PlatformResponseDto platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var platformResponseDto = await _service.Update(platform);

            return CreatedAtRoute(nameof(GetPlatformByIdAsync), new { platformResponseDto.Id }, platformResponseDto);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PlatformResponseDto>> DeletePlatformAsync(int id)
        {
            var platformResponseDto = await _service.GetById(id);

            await _service.Delete(id);

            return CreatedAtRoute(nameof(GetPlatformByIdAsync), new { Id = id }, platformResponseDto);
        }
    }
}
