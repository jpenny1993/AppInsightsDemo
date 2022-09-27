using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AppInsights.FunctionApp.Functions;

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