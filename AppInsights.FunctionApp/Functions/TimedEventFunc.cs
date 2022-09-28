using System;
using System.Net.Http.Json;
using AppInsights.FunctionApp.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AppInsights.FunctionApp
{
    public class TimedEventFunc
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfig;


        public TimedEventFunc(
            ILoggerFactory loggerFactory,
            IOptions<ApiConfiguration> apiOptions,
            IHttpClientFactory httpClientFactory)
        {
            _logger = loggerFactory.CreateLogger<TimedEventFunc>();
            _httpClient = httpClientFactory.CreateClient();
            _apiConfig = apiOptions.Value;
        }

        [Function("TimedEventFunc")]
        public async Task RunAsync([TimerTrigger("0 */10 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation("C# Timer trigger function executed at: {ExecutionStartTime}", DateTime.Now);
            _logger.LogInformation("Next timer schedule at: {NextSheduledRunTime}", myTimer.ScheduleStatus.Next);

            var result = await _httpClient.GetFromJsonAsync<string>($"{_apiConfig.Url}/math/fact?key={_apiConfig.Key}");

            _logger.LogInformation("That's all folks");
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
