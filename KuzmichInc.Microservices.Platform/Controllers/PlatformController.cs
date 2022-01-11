using AutoMapper;
using KuzmichInc.Microservices.PlatformService.Dtos;
using KuzmichInc.Microservices.PlatformService.Models;
using KuzmichInc.Microservices.PlatformService.Repositories.Base;
using KuzmichInc.Microservices.PlatformService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KuzmichInc.Microservices.PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IRepository<Platform> _repository;
        private readonly IMapper _mapper;

        public PlatformController(IRepository<Platform> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformDto>> GetPlatforms()
        {
            var platforms = _repository.GetAll();
            var mappedPlatforms = _mapper.Map<IEnumerable<PlatformDto>>(platforms);

            return Ok(mappedPlatforms);
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformDto> GetPlatformById(int id)
        {
            var platform = _repository.GetById(id);
            if (platform is null)
            {
                return NotFound();
            }

            var mappedPlatform = _mapper.Map<PlatformDto>(platform);
            return Ok(mappedPlatform);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PlatformViewModel> CreatePlatform(PlatformViewModel platformViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappedPlatform = _mapper.Map<Platform>(platformViewModel);

            var platform = _repository.Create(mappedPlatform);
            
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = mappedPlatform.Id }, platform);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PlatformViewModel> UpdatePlatform(PlatformViewModel platformViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappedPlatform = _mapper.Map<Platform>(platformViewModel);

            var platform = _repository.Update(mappedPlatform);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = mappedPlatform.Id }, platform);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PlatformViewModel> DeletePlatform(int id)
        {
            var platform = _repository.GetById(id);

            _repository.Delete(platform);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);
        }
    }
}
