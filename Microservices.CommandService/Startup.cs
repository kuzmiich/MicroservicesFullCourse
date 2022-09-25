using System;
using Microservices.CommandService.Data;
using Microservices.CommandService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Microservices.CommandService
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddDbContext(services);
            services.AddScoped<ICommandRepository, CommandRepository>();
            
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KuzmichInc.Microservices.CommandsService", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                    "KuzmichInc.Microservices.CommandsService v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        
        private void AddDbContext(IServiceCollection services)
        {
            if (_env.IsProduction())
            {
                services.AddDbContext<CommandContext>(options =>
                    options.UseSqlServer(_configuration.GetConnectionString("MSSqlDatabaseProduction")));
            }
            else if (_env.IsDevelopment())
            {
                services.AddDbContext<CommandContext>(options =>
                    options.UseInMemoryDatabase("InMemory"));
                //options.UseSqlServer(_configuration.GetConnectionString("MSSqlDatabaseLocal")));
            }
        }
    }
}
