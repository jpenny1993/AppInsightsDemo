using AppInsights.WorkerService;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureTelemetryModule<QuickPulseTelemetryModule>((module, o) => module.AuthenticationApiKey = host.configuration["APPINSIGHTS_QUICKPULSEAUTHAPIKEY"]);
    })
    .Build();

await host.RunAsync();