using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microservices.CommandService.Dtos;
using Microservices.CommandService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandService _service;

        public PlatformController(IMapper mapper, ICommandService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlatformReadDto>>> GetPlatforms()
        {
            var platforms = await _service.GetAllPlatforms();
            
            return Ok(_mapper.Map<List<PlatformReadDto>>(platforms));
        }
    }
}