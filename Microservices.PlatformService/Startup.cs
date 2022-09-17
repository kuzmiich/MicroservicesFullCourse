using AutoMapper;
using Microservices.Repositories;
using Microservices.Services;
using Microservices.PlatformService.Data;
using Microservices.PlatformService.Dtos;
using Microservices.PlatformService.Models;
using Microservices.PlatformService.Profiles;
using Microservices.PlatformService.Repositories;
using Microservices.PlatformService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Microservices.PlatformService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PlatformContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("MSSqlDatabase")),
                options.UseInMemoryDatabase("InMemory"),
                ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWorkRepository<Platform>, PlatformRepository>();
            services.AddScoped<IUnitOfWorkService<PlatformResponseDto, PlatformRequestDto>, PlatformBusinessService>();

            services.AddControllers();
            services.AddAutoMapper(configuration => configuration.AddProfiles(new Profile[]
            {
                new PlatformProfiles()
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Platform", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Platform v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
