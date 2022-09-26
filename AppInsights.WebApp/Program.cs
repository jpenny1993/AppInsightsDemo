using DomainLogic;

var builder = WebApplication.CreateBuilder(args);

// Configure by setting "APPLICATIONINSIGHTS_CONNECTION_STRING" in configuration blade
// or by setting "ApplicationInsights:ConnectionString" in appsettings.json
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddTransient<FactService>();
builder.Services.AddTransient<MathService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/math/{a:decimal}/add/{b:decimal}", (decimal a, decimal b, MathService mathService) => mathService.Add(a, b));

app.MapGet("/math/{a:decimal}/subtract/{b:decimal}", (decimal a, decimal b, MathService mathService) => mathService.Subtract(a, b));

app.MapGet("/math/{a:decimal}/multiply/{b:decimal}", (decimal a, decimal b, MathService mathService) => mathService.Multiply(a, b));

app.MapGet("/math/{a:decimal}/divide/{b:decimal}", (decimal a, decimal b, MathService mathService) => mathService.Divide(a, b));

app.MapGet("/math/fact", async (FactService domainService) => await domainService.GetMathFact());

app.Run();