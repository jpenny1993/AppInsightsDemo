using DomainLogic;
using Microsoft.AspNetCore.Http.Json;
using Pokedex;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.WriteIndented = true;
});

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddTransient<FactService>();
builder.Services.AddTransient<MathService>();
builder.Services.AddDbContext<PokedexDbContext>();

var app = builder.Build();

app.MapGet("/favicon.ico", () => Results.NotFound());

app.MapGet("/", () => "Hello World!");

app.MapGet("/math/{a:decimal}/add/{b:decimal}",
    (decimal a, decimal b, MathService mathService) => mathService.Add(a, b));

app.MapGet("/math/{a:decimal}/subtract/{b:decimal}",
    (decimal a, decimal b, MathService mathService) => mathService.Subtract(a, b));

app.MapGet("/math/{a:decimal}/multiply/{b:decimal}",
    (decimal a, decimal b, MathService mathService) => mathService.Multiply(a, b));

app.MapGet("/math/{a:decimal}/divide/{b:decimal}",
    (decimal a, decimal b, MathService mathService) => mathService.Divide(a, b));

app.MapGet("/math/fact",
    async (FactService factService) => await factService.GetMathFact());

app.MapGet("throw", () => { throw DomainException.Default; });

app.MapGet("/pokemon",
    (PokedexDbContext ctx) => ctx.Pokemons.Select(x => new { Id = x.NationalNumber, x.Name }));

app.MapGet("/pokemon/{id:int}",
    (int id, PokedexDbContext ctx) => ctx.Pokemons.Find(id));

app.Run();