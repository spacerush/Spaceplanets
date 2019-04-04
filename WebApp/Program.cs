using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sentry;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // IHostingEnvironment is unavailable in a console app:
            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";

            var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (isDevelopment)
            {
                // The following line scans the assembly that contains the "Program" type for an instane of UserSecretsIdAttribute
                configBuilder.AddUserSecrets<Program>();
            }
            IConfigurationRoot configuration = configBuilder.Build();
            string dsn = configuration["SentrySdk:DSN"];
            using (SentrySdk.Init(dsn))
            {
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>();
    }
}
