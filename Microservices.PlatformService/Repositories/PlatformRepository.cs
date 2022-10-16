using Microservices.Repositories;
using Microservices.PlatformService.Data;
using Microservices.PlatformService.Models;

namespace Microservices.PlatformService.Repositories
{
    public class PlatformRepository : BaseCrudRepository<PlatformsContext, Platform>
    {
        public PlatformRepository(PlatformsContext context) : base(context)
        {
        }
    }
}
