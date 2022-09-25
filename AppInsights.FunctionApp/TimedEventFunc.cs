using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AppInsights.FunctionApp;

public static class TimedEventFunc
{
    [FunctionName("TimedEventFunc")]
    public static async Task RunAsync(
        [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
        ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
        
    }
}

// using
//     Microsoft.Azure.WebJobs.Logging.ApplicationInsights.WebJobsRoleEnvironmentTelemetryInitializer
// Microsoft.Azure.WebJobs.Logging.ApplicationInsights.WebJobsTelemetryInitializer

// return services
//     .AddSingleton(sp =>
//     {
//         var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
//         telemetryConfiguration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
//         var config = sp.GetService<IConfiguration>();
//         var instrumentationKey = config.GetValue<string?>("APPINSIGHTS_INSTRUMENTATIONKEY");
//         if (instrumentationKey != null)
//             telemetryConfiguration.InstrumentationKey = instrumentationKey;
//         return telemetryConfiguration;
//     });