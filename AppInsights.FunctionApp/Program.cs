using AppInsights.FunctionApp.Configuration;
using Microsoft.ApplicationInsights;
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
        .Configure<ApiConfiguration>(host.Configuration.GetSection("ApiConfiguration"))
        .AddSingleton<ILoggerProvider>((sp) =>
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.With<CorrelationIdEnricher>()
                .WriteTo.ApplicationInsights(sp.GetRequiredService<TelemetryClient>(), TelemetryConverter.Traces)
                .CreateLogger();
            return new SerilogLoggerProvider(Log.Logger, true);
        })
    )
    .Build();

host.Run();
