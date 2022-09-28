using AppInsights.FunctionApp.Configuration;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers;
using Serilog.Exceptions;
using Serilog.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((host, services) => services
        .AddOptions()
        .AddSingleton<ILoggerProvider>((sp) =>
        {
            var telemetryClient = new TelemetryClient(TelemetryConfiguration.CreateDefault());
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.With<CorrelationIdEnricher>()
                .WriteTo.ApplicationInsights(telemetryClient, TelemetryConverter.Traces)
                .CreateLogger();
            return new SerilogLoggerProvider(Log.Logger, true);
        })
        .AddHttpClient("DemoApi", httpClient =>
        {
            var config = host.Configuration.GetSection("ApiConfiguration").Get<ApiConfiguration>();
            httpClient.BaseAddress = new Uri(config.Url);
        })
    )
    .Build();

host.Run();
