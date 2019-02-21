using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Mapper;
using GameStore.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;

namespace GameStore.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            InjectionResolver.ConfigurateInjections(services);

            services.AddMvc().AddJsonOptions(option =>
         option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "GameStore",
                    Description = "ASP.NET Core Web API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(config =>
            {
                config.AddProfile<MapToBLModels>();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("api", "api/games", new { controller = "Games", action = "Get" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Games}/{action=Get}/{id?}");
            });
        }
    }
}
