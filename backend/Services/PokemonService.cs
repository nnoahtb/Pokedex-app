using System.Net;
using System.Text.Json;
using Backend.Models;

namespace Backend.Services;

public class PokemonService(HttpClient _http, ILogger<PokemonService> _logger)
{
    public async Task<PokemonDto?> GetPokemonAsync(string idOrName, CancellationToken ct = default)
    {
        var cleaned = idOrName.Trim().ToLower();
        var url = "https://pokeapi.co/api/v2/pokemon/" + WebUtility.UrlEncode(cleaned);

        _logger.LogInformation("Calling PokeAPI: {Url}", url);

        var response = await _http.GetAsync(url, ct);

        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            _logger.LogError("Error from PokeAPI for {IdOrName}", idOrName);
            throw new Exception();
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Pokemon not found: {IdOrName}", idOrName);
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync(ct);
        var pokeResponse = JsonSerializer.Deserialize<PokeApiResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (pokeResponse == null)
        {
            _logger.LogError("Deserialization failed for {IdorName}", idOrName);
            throw new Exception();
        }

        var dto = new PokemonDto
        {
            Id = pokeResponse.id,
            Name = pokeResponse.name,
            Image = pokeResponse.sprites.front_default
        };

        return dto;
    }
}