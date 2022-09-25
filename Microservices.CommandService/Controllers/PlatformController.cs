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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
        {
            var platforms = await _repository.GetAllPlatforms().ToListAsync();
            
            return Ok(_mapper.Map<PlatformReadDto>(platforms));
        }
    }
}