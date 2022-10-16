using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.PlatformService.Models;

namespace Microservices.PlatformService.Data
{
    public class PlatformsContext : DbContext
    {
        public PlatformsContext(DbContextOptions<PlatformsContext> options) : base(options)
        {
        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
