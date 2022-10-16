using Microservices.Repositories;
using Microservices.PlatformService.Data;
using Microservices.PlatformService.Models;

namespace Microservices.PlatformService.Repositories
{
    public class PlatformCrudRepository : BaseCrudRepository<PlatformsContext, Platform>
    {
        public PlatformCrudRepository(PlatformsContext context) : base(context)
        {
        }
    }
}
