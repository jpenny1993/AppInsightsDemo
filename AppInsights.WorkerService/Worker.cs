using DomainLogic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace AppInsights.WorkerService;

public class Worker : BackgroundService
{
    private static readonly Random Random = new ();
    private readonly ILogger<Worker> _logger;
    private readonly TelemetryClient _telemetryClient;
    private readonly FactService _factService;

    public Worker(
        ILogger<Worker> logger,
        TelemetryClient telemetryClient,
        FactService factService)
    {
        _logger = logger;
        _telemetryClient = telemetryClient;
        _factService = factService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);

            using (_telemetryClient.StartOperation<RequestTelemetry>("worker operation"))
            {
                _logger.LogWarning("A sample warning message...");
                var factCount = Random.Next(1, 15);
                var index = 1;
                await foreach (var fact in _factService.GetMathFacts(factCount, stoppingToken))
                {
                    _logger.LogInformation("Fact {FactIndex}: {MathFact}", index, fact);
                    index++;
                }

                _telemetryClient.TrackEvent("Math facts acquired");
            }

            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }
}