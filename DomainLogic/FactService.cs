using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace DomainLogic;

public class FactService
{
    private const string MathFactUri = "http://numbersapi.com/random/math";
    private static readonly HttpClient HttpClient = new ();
    private readonly ILogger<FactService> _logger;

    public FactService(ILogger<FactService> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetMathFact()
    {
        using (_logger.BeginScope(GetScopeInformation()))
        {
            _logger.LogInformation("Executing {FunctionName}", nameof(GetMathFact));
            var response = await HttpClient.GetAsync(MathFactUri);
            
            _logger.LogInformation("Http request completed with status: {HttpStatusCode}", response.StatusCode);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

    public async IAsyncEnumerable<string> GetMathFacts(int count, [EnumeratorCancellation]CancellationToken cancellationToken)
    {
        _logger.LogInformation("Requesting {MathFactRequestCount} math facts", count);
        for (var requestIndex = 0; requestIndex < count; requestIndex++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            yield return await GetMathFact();
        }
    }

    private static Dictionary<string, object> GetScopeInformation()
    {
        return new()
        {
            { "MachineName", Environment.MachineName },
            { "ProcessId", Environment.ProcessId },
            { "RuntimeVersion", Environment.Version },
            { "ThreadId", Environment.CurrentManagedThreadId },
            { "Username", Environment.UserName }
        };
    }
}