using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotApiGw
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    if (env == "" || env== null)
                    {
                        webBuilder.ConfigureAppConfiguration(config =>
                        config.AddJsonFile($"ocelot.json"));
                    }
                    else
                    {
                        webBuilder.ConfigureAppConfiguration(config =>
                        config.AddJsonFile($"ocelot.{env}.json"));
                    }
                })
            .ConfigureLogging(logging => logging.AddConsole());
    }
}
