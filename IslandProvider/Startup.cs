using Akka.Actor;
using Akka.Configuration;
using IslandRouter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace IslandProvider
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Island router API", Version = "v1" });
            });

            Config akkaSystemConfig = ReadAkkaConfig();
            services.AddSingleton(_ => ActorSystem.Create("island-router-system", akkaSystemConfig));
            services.AddSingleton(akkaSystemConfig);
            services.AddSingleton<IslandRouterService>();
        }

        private Config ReadAkkaConfig(string configFile = "akka.conf")
        {
            Config config = Config.Empty;
            if (File.Exists(configFile))
            {
                string configText = File.ReadAllText(configFile);
                config = ConfigurationFactory.ParseString(configText);
            }
            return config;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, IslandRouterService islandRouterService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            lifetime.ApplicationStarted.Register(() =>
            {
                app.ApplicationServices.GetService<ActorSystem>();
            });
            lifetime.ApplicationStopping.Register(() =>
            {
                app.ApplicationServices.GetService<ActorSystem>().Terminate().Wait();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Island router API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}