using Backend.Middleware;
using Backend.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

builder.Services.AddHttpClient<PokemonService>();

builder.Services.AddCors(Options =>
{
    Options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.MapGet("/api/pokemon/{idOrName}", async (string idOrName, PokemonService service, CancellationToken ct) =>
{
    var result = await service.GetPokemonAsync(idOrName, ct);
    var response = result is null
        ? Results.NotFound(new { message = $"Pokémon '{idOrName}' hittades inte." })
        : Results.Ok(result);

    return response;
});

app.Run();
