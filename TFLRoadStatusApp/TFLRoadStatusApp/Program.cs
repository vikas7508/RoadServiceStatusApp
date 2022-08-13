using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; 
using Serilog; 
using System;
using TflRoadStatusApp.Helpers; 
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using TflRoadStatusApp.Helpers.Interfaces;

namespace TflRoadStatusApp
{
    static class Program
    {
        static int Main(string[] args)
        {            
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            IConfiguration config = builder.Build();
            if(bool.Parse(config["EnableLogging"]))
            {
                Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(config)
                        .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
            }            

            return Host.CreateDefaultBuilder()
                .ConfigureLogging( cfg=> cfg.ClearProviders()) 
                 .ConfigureServices(services => services
                 .AddSingleton<ITflRoadMessageHelper, TflRoadMessageHelper>()
                 .AddSingleton<ITflRoadAppMain, TflRoadAppMain>()
                 .AddHttpClient<ITflRoadService, TflRoadService>(c =>
                 {
                     c.BaseAddress = new Uri(config["BaseUrl"]);
                 }))                 
                 .UseSerilog()
                 .Build()
                 .Services
                 .GetService<ITflRoadAppMain>()
                .RunAsync(args).Result;
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {           
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

    }
}
