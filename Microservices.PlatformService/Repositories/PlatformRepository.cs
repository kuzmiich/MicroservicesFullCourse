using Microservices.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microservices.PlatformService.Data;
using Microservices.PlatformService.Models;

namespace Microservices.PlatformService.Repositories
{
    public class PlatformRepository : BaseRepository<PlatformContext, Platform>
    {
        public PlatformRepository(PlatformContext context) : base(context)
        {
        }
    }
}
