using Akka.Actor;
using Akka.Configuration;
using GeneticCore;
using GeneticSharp.Domain.Randomizations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Island
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            Config akkaSystemConfig = ReadAkkaConfig();

            services.AddSingleton(_ => ActorSystem.Create($"Island - {Environment.MachineName} [{Guid.NewGuid()}]", akkaSystemConfig));
            services.AddSingleton<GeneticCoreSerivce>();
            services.AddSingleton<IRandomization, FastRandomRandomization>();
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            lifetime.ApplicationStarted.Register(() =>
            {
                app.ApplicationServices.GetService<ActorSystem>();
            });
            lifetime.ApplicationStopping.Register(() =>
            {
                app.ApplicationServices.GetService<ActorSystem>().Terminate().Wait();
            });
        }
    }
}