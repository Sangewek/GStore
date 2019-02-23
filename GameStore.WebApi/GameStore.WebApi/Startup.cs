using System;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Mapper;
using GameStore.WebApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using GameStore.WebApi.Mapper;
using Microsoft.Extensions.PlatformAbstractions;

namespace GameStore.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0] + "Properties";
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            InjectionResolver.ConfigurateInjections(services, connectionString);

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
            services.AddSwaggerGen();

            services.AddMvc(
                config =>
                {
                    config.Filters.Add(new ActionFilters());
                });

            services.ConfigureSwaggerGen(options =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;

                var xmlPath = Path.Combine(basePath, "GameStore.WebApi.xml");
                options.IncludeXmlComments(xmlPath);
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
                config.AddProfile<MapToViewModels>();
            });

            app.UseMiddleware<ExceptionMiddleware>();

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
