using System;
using AutoMapper;
using Microservices.Repositories;
using Microservices.Services;
using Microservices.PlatformService.Data;
using Microservices.PlatformService.Dtos;
using Microservices.PlatformService.Models;
using Microservices.PlatformService.Profiles;
using Microservices.PlatformService.Repositories;
using Microservices.PlatformService.Services;
using Microservices.PlatformService.SyncDataServices.Grpc;
using Microservices.PlatformService.SyncDataServices.Http;
using Microservices.PlatformService.SyncDataServices.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace Microservices.PlatformService
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            AddDbContext(services);
            services.AddScoped<IUnitOfWorkRepository<Platform>, PlatformRepository>();
            services.AddScoped<IUnitOfWorkService<PlatformReadDto, PlatformCreateDto>, PlatformBusinessService>();
            services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();

            services.AddGrpc();
            services.AddControllers();
            services.AddAutoMapper(configuration => configuration.AddProfiles(new Profile[]
            {
                new PlatformProfiles()
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Microservices.Platform", Version = "v1" });
            });
            
            // Logger
            Console.WriteLine($"--> CommandService Endpoint {_configuration["CommandServiceURL"]}");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Platform v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GrpcPlatformService>();

                endpoints.MapGet("/protos/platforms.proto", async context =>
                    await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto")));
            });
        }
        
        private void AddDbContext(IServiceCollection services)
        {
            if (_env.IsProduction())
            {
                services.AddDbContext<PlatformsContext>(options =>
                    options.UseSqlServer(_configuration.GetConnectionString("MSSqlDatabaseProduction")));
            }
            else if (_env.IsDevelopment())
            {
                services.AddDbContext<PlatformsContext>(options =>
                    options.UseSqlServer(_configuration.GetConnectionString("MSSqlDatabaseLocal")));
            }
        }
    }
}
