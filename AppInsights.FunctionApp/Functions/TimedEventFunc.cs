using System.Net.Http.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AppInsights.FunctionApp
{
    public class TimedEventFunc
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;


        public TimedEventFunc(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory)
        {
            _logger = loggerFactory.CreateLogger<TimedEventFunc>();
            _httpClient = httpClientFactory.CreateClient("DemoApi");
        }

        [Function("TimedEventFunc")]
        public async Task RunAsync([TimerTrigger("0 */10 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation("C# Timer trigger function executed at: {ExecutionStartTime}", DateTime.Now);
            _logger.LogInformation("Next timer schedule at: {NextSheduledRunTime}", myTimer.ScheduleStatus.Next);

            var result = await _httpClient.GetStringAsync("/math/fact");
            _logger.LogInformation("Acquired fact: {MathFact}", result);

            var response = await _httpClient.GetAsync("/throw");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Request failed with http status: {HttpStatusCode}", response.StatusCode);
            }
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
