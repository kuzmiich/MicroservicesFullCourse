using KuzmichInc.Microservices.PlatformService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.Data
{
    public class PlatformContext : DbContext
    {
        public PlatformContext(DbContextOptions<PlatformContext> options) : base(options)
        {
        }

        public DbSet<Platform> Platforms { get; set; }
    }
}
