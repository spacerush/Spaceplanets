﻿using Microsoft.AspNetCore;
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
    public class CustomFilter : IFilter
    {
        private IHealthReporter HealthReporter;
        private string MachineName;
        public CustomFilter(string ServerName, IHealthReporter HealthReporter)
        {
            MachineName = ServerName;
            this.HealthReporter = HealthReporter;
        }

        FilterResult IFilter.Evaluate(EventData eventData)
        {
            eventData.AddPayloadProperty("ServerName", MachineName, HealthReporter, "CustomFilter");
            return FilterResult.DiscardEvent;
        }
    }

    public class CustomFilterFactory : IPipelineItemFactory<CustomFilter>
    {
        public CustomFilter CreateItem(IConfiguration configuration, IHealthReporter healthReporter)
        {
            CustomFilter filter = new CustomFilter(System.Environment.MachineName, healthReporter);
            return filter;
        }
    }
}
