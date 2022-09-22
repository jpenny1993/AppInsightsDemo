using Microsoft.Extensions.Logging;

namespace DomainLogic;

public class DomainService
{
    private readonly ILogger<DomainService> _logger;

    public DomainService(ILogger<DomainService> logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.LogInformation("Some amount of work has been done");
    }
}