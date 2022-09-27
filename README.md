# Using Application Insights in .NET

### Note for existing projects

> On March 31, 2025, support for instrumentation key ingestion will end.
> Instrumentation key ingestion will continue to work, but will no longer be provided updates or support for the feature.
> To continue taking advantage of new capabilities, projects should be transitioned to [use connection strings](https://learn.microsoft.com/en-us/azure/azure-monitor/app/migrate-from-instrumentation-keys-to-connection-strings).

## ASP.NET Core apps

1. Add the following NuGet package to your main project `Microsoft.ApplicationInsights.AspNetCore`
2. In the service configuration add the following IServiceCollection extension `.AddApplicationInsightsTelemetry()`
3. In the `appsettings.json` configure `ApplicationInsights:ConnectionString` and use the connection string to your application insights resource in the Azure Portal
4. Alternatively you can configure `APPLICATIONINSIGHTS_CONNECTION_STRING` in the configuration blade of your app service resource

For further information see [Application Insights for ASP.NET Core applications](https://learn.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core)

### Adding telemetry to front-end websites 

This will depend on what framework your website is built in.

For ASP.NET Core websites setup is incredibly simple, for further information see [Enable client-side telemetry for web applications](https://learn.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core?tabs=netcore6#enable-client-side-telemetry-for-web-applications)

1. Add the following line to `_ViewImports.cshtml`
   ```cshtml
   @inject Microsoft.ApplicationInsights.AspNetCore.JavaScriptSnippet JavaScriptSnippet
   ```
2. In `_Layout.cshtml` add the following line before the closing `</head>` tag
   ```cshtml
   @Html.Raw(JavaScriptSnippet.FullScript)
   ```

For Single Page Applications written in Javascript it's a bit more involved, for further information see [Application Insights for webpages](https://learn.microsoft.com/en-us/azure/azure-monitor/app/javascript)

1. Add the following npm package to your project `@microsoft/applicationinsights-web`
2. Add the following javascript snippet to configure application insights
   ```javascript
   import { ApplicationInsights } from '@microsoft/applicationinsights-web'
   
   const appInsights = new ApplicationInsights({ config: {
   connectionString: 'Copy connection string from Application Insights Resource Overview'
   /* ...Other Configuration Options... */
   } });
   appInsights.loadAppInsights();
   appInsights.trackPageView(); // Manually call trackPageView to establish the current user/session/pageview
   ```
3. When a navigation event is sent to the router, you will need to call `.trackPageView()`
4. To send log messages to application insights use `.trackTrace({message: 'Example log message'});`

## Azure Function apps

Application insights is built into Azure Function apps by default, but correlation is not included out of the box.

To setup default logging configure `APPLICATIONINSIGHTS_CONNECTION_STRING` in the configuration blade of your azure function resource.

## Worker services & console apps

1. Add the following NuGet package to your main project `Microsoft.ApplicationInsights.WorkerService`
2. In the service configuration add the following IServiceCollection extension `.AddApplicationInsightsTelemetryWorkerService()`
3. In the `appsettings.json` configure `ApplicationInsights:ConnectionString` and use the connection string to your application insights resource in the Azure Portal
4. Alternatively you can configure `APPLICATIONINSIGHTS_CONNECTION_STRING` as an environment variable, this can be set on Web Apps and Web Jobs

For further information see [Application Insights for Worker Service applications (non-HTTP applications)](https://learn.microsoft.com/en-us/azure/azure-monitor/app/worker-service)

## Alternative configuration for console apps

It's no longer recommended by Microsoft to create your own instance of TelemetryClient, but it is still possible to do so for LTS of older apps.

1. Add the following NuGet package to your main project `Microsoft.ApplicationInsights`
2. Add the following lines to the start of your application, you should keep `telemetryClient` as a singleton in you application 
   ```csharp
   var configuration = TelemetryConfiguration.CreateDefault();
   configuration.ConnectionString = "PUT CONNECTION STRING HERE";
   var telemetryClient = new TelemetryClient(configuration);
   ```
4. Manual setup of App insights like this will not automatically include things like dependency collection or correlation, these must be configured manually. Install the NuGet package `Microsoft.ApplicationInsights.DependencyCollector` and add the following code, see a [full example here](https://learn.microsoft.com/en-us/azure/azure-monitor/app/console#full-example)
   ```csharp
   configuration.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());
   configuration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
   ```
5. Finally when terminating your application use `telemetryClient.Flush();` to send any pending logs to app insights
6. Depending on the telemetry channel being used it's also wise to add a 5 second delay, the default server telemetry channel used by most packages can only flush logs asynchronously, however if using an in-memory telemetry channel this is not required  

For more information see [Application Insights for .NET console applications](https://learn.microsoft.com/en-us/azure/azure-monitor/app/console)

## .NET Framework apps

Add the following lines to your application. Calling `TelemetryConfiguration.Active` will use the connection string / instrumentation key from `ApplicationInsights.config`

   ```csharp
   var configuration = TelemetryConfiguration.Active;
   var telemetryClient = new TelemetryClient(configuration);
   ```