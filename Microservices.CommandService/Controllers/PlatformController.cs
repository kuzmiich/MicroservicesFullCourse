using AutoMapper;
using Microservices.PlatformService.Models;
using Microservices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.PlatformService.Dtos;

namespace Microservices.CommandsService.Controllers
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
        public async Task<ActionResult<IEnumerable<PlatformResponseDto>>> GetPlatformsAsync()
        {
            var platforms = await _service.GetAllAsync();

            return Ok(platforms);
        }
    }
}