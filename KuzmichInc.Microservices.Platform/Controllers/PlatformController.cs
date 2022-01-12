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
        private readonly IDtoService<PlatformResponseDto> _service;
        private readonly IMapper _mapper;

        public PlatformController(IDtoService<PlatformResponseDto> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Platform>>> GetPlatformsAsync()
        {
            var platforms = await _service.GetAll();

            return Ok(platforms);
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<ActionResult<Platform>> GetPlatformByIdAsync(int id)
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
        public async Task<ActionResult<PlatformResponseDto>> CreatePlatformAsync(Platform platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappedPlatform = _mapper.Map<PlatformResponseDto>(platform);
            var platformDto = await _service.Create(mappedPlatform);
            
            return CreatedAtRoute(nameof(GetPlatformByIdAsync), new { Id = platform.Id }, platformDto);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlatformResponseDto>> UpdatePlatformAsync(Platform platform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappedPlatform = _mapper.Map<PlatformResponseDto>(platform);
            var platformDto = await _service.Update(mappedPlatform);

            return CreatedAtRoute(nameof(GetPlatformByIdAsync), new { Id = platform.Id }, platformDto);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PlatformResponseDto>> DeletePlatformAsync(int id)
        {
            var platformDto = await _service.GetById(id);

            await _service.Delete(id);

            return CreatedAtRoute(nameof(GetPlatformByIdAsync), new { Id = id }, platformDto);
        }
    }
}
