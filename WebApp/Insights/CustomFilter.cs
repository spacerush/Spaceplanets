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

namespace WebApp
{
    class CustomGlobalFilter : IFilter
    {
        private IHealthReporter HealthReporter;
        private string MachineName;
        public CustomGlobalFilter(string ServerName, IHealthReporter HealthReporter)
        {
            MachineName = ServerName;
            this.HealthReporter = HealthReporter;
        }

        FilterResult IFilter.Evaluate(EventData eventData)
        {
            eventData.AddPayloadProperty("ServerName", MachineName, HealthReporter, "CustomGlobalFilter");
            return FilterResult.KeepEvent;
        }
    }

    class CustomGlobalFilterFactory : IPipelineItemFactory<CustomGlobalFilter>
    {
        public CustomGlobalFilter CreateItem(IConfiguration configuration, IHealthReporter healthReporter)
        {
            CustomGlobalFilter GlobalFilter = new CustomGlobalFilter(System.Environment.MachineName, healthReporter);
            return GlobalFilter;
        }
    }
}
