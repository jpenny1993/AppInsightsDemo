using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppInsights.FunctionApp;

public static class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices((host, services) => services
                   .AddOptions()
                   .AddLogging()
              ).Build();

        host.Run();
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