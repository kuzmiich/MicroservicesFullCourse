using AutoMapper;
using Microservices.PlatformService.Models;
using Microservices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.CommandService.SyncDataServices.Http;
using Microservices.PlatformService.Dtos;

namespace Microservices.CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly ICommandDataClient _service;
        private readonly IMapper _mapper;

        public PlatformController(ICommandDataClient service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> GetPlatformsFiltered()
        {
            return null;
        }
    }
}