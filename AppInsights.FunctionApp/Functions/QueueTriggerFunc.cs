using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AppInsights.FunctionApp.Functions;

public static class QueueTriggerFunc
{
    [FunctionName("QueueTriggerFunc")]
    public static async Task RunAsync([ServiceBusTrigger("myqueue", Connection = "")] string myQueueItem, ILogger log)
    {
        log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        
    }
}