using OpenTelemetry.Logs;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Extensions.Hosting;
using OpenTelemetry.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace AudioProcessing.API.DiConfiguration
{
    public static class OpenTelemetry
    {

        public static IServiceCollection AddOpenTelemetry(this IServiceCollection services)
        {
            
            return services;
        }

    }
}
