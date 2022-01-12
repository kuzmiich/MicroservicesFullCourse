using KuzmichInc.Microservices.PlatformService.Data;
using KuzmichInc.Microservices.PlatformService.Models;
using KuzmichInc.Microservices.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Repositories
{
    public class PlatformRepository : BaseRepository<PlatformContext, Platform>
    {
        public PlatformRepository(PlatformContext context) : base(context)
        {
        }
    }
}
