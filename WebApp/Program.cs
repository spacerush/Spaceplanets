using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Diagnostics.EventFlow;
using Microsoft.Diagnostics.EventFlow.ApplicationInsights;
using Microsoft.Diagnostics.EventFlow.HealthReporters;
using Microsoft.Diagnostics.EventFlow.Inputs;
using Microsoft.Diagnostics.EventFlow.Outputs;
using Microsoft.Diagnostics.EventFlow.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.AspNetCore;
using Sentry;
using System.Collections.Generic;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var eventFlow = CreateEventFlow(args))
            {
                BuildWebHost(args, eventFlow).Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args, DiagnosticPipeline eventFlow) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddSingleton<ITelemetryProcessorFactory>(sp => new EventFlowTelemetryProcessorFactory(eventFlow)))
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

        private static DiagnosticPipeline CreateEventFlow(string[] args)
        {
            // Create configuration instance to access configuration information for EventFlow pipeline
            // To learn about common configuration sources take a peek at https://github.com/aspnet/MetaPackages/blob/master/src/Microsoft.AspNetCore/WebHost.cs (CreateDefaultBuilder method). 
            var configBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            var devEnvironmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                    devEnvironmentVariable.ToLower() == "development";

            if (isDevelopment)
            {
                configBuilder.AddUserSecrets<Program>();
            }

            if (args != null)
            {
                configBuilder.AddCommandLine(args);
            }
            var config = configBuilder.Build();

            // SEE https://github.com/Azure/diagnostics-eventflow#http
            var httpConfig = config.GetSection("HttpEventSinkConfig");
            var filterConfig = config.GetSection("FilterConfig");
            var healthReporter = new CsvHealthReporter(new CsvHealthReporterConfiguration());
            var aiInput = new ApplicationInsightsInputFactory().CreateItem(null, healthReporter);
            var aiFilters = new CustomFilterFactory().CreateItem(filterConfig, healthReporter);
            var inputs = new IObservable<EventData>[] { aiInput };
            var filters = new IFilter[] { aiFilters };

            var sinks = new EventSink[]
            {
                new EventSink(new StdOutput(healthReporter), null),
                new EventSink(new HttpOutput(httpConfig, healthReporter), null) // again, see https://github.com/Azure/diagnostics-eventflow#http
            };

            return new DiagnosticPipeline(healthReporter, inputs, filters, sinks, null, disposeDependencies: true);
        }
    }
}
