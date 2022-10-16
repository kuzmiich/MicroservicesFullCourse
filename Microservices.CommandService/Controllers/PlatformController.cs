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
        private readonly ICommandRepository _repository;

        public PlatformController(IMapper mapper, ICommandRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PlatformReadDto>>> GetPlatforms()
        {
            var platforms = await _repository.GetAllPlatforms();
            
            return Ok(_mapper.Map<List<PlatformReadDto>>(platforms));
        }
    }
}