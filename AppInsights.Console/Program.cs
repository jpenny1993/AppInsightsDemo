﻿using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;

namespace AppInsights.Console;

public static class Program
{
   static void Main(string[] args)
   {
      var configuration = TelemetryConfiguration.CreateDefault();

      configuration.ConnectionString = "PutConnectionStringHere";
      configuration.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());

      var telemetryClient = new TelemetryClient(configuration);
      using (InitializeDependencyTracking(configuration))
      {
            // run app...
            telemetryClient.TrackTrace("Hello World!");

            using (var httpClient = new HttpClient())
            {
               // Http dependency is automatically tracked!
               httpClient.GetAsync("https://microsoft.com").Wait();
            }

      }

      // before exit, flush the remaining data
      telemetryClient.Flush();

      // Console apps should use the WorkerService package.
      // This uses ServerTelemetryChannel which does not have synchronous flushing.
      // For this reason we add a short 5s delay in this sample.
      
      Task.Delay(5000).Wait();

      // If you're using InMemoryChannel, Flush() is synchronous and the short delay is not required.
   }

   static DependencyTrackingTelemetryModule InitializeDependencyTracking(TelemetryConfiguration configuration)
   {
      var module = new DependencyTrackingTelemetryModule();

      // prevent Correlation Id to be sent to certain endpoints. You may add other domains as needed.
      module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.windows.net");
      module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.chinacloudapi.cn");
      module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.cloudapi.de");
      module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("core.usgovcloudapi.net");
      module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("localhost");
      module.ExcludeComponentCorrelationHttpHeadersOnDomains.Add("127.0.0.1");

      // enable known dependency tracking, note that in future versions, we will extend this list. 
      // please check default settings in https://github.com/microsoft/ApplicationInsights-dotnet-server/blob/develop/WEB/Src/DependencyCollector/DependencyCollector/ApplicationInsights.config.install.xdt

      module.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.ServiceBus");
      module.IncludeDiagnosticSourceActivities.Add("Microsoft.Azure.EventHubs");

      // initialize the module
      module.Initialize(configuration);

      return module;
   }
}