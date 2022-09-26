using Microsoft.Extensions.Logging;

namespace DomainLogic;

public class MathService
{
    private readonly ILogger<MathService> _logger;

    public MathService(ILogger<MathService> logger)
    {
        _logger = logger;
    }

    public decimal Add(decimal a, decimal b)
    {
        _logger.LogInformation("Adding values {ParamA} and {ParamB}", a, b);
        return a + b;
    }
    
    public decimal Subtract(decimal a, decimal b)
    {
        _logger.LogInformation("Subtracting values {ParamB} from {ParamA}", b, a);
        return a - b;
    }
    
    public decimal Multiply(decimal a, decimal b)
    {
        _logger.LogInformation("Multiplying values {ParamA} and {ParamB}", a, b);
        return a * b;
    }
    
    public decimal Divide(decimal a, decimal b)
    {
        _logger.LogInformation("Dividing values {ParamB} from {ParamA}", b, a);
        if (b == 0)
        {
            _logger.LogWarning("Handled divide by zero exception");
            return 0;
        }

        return a / b;
    }
}