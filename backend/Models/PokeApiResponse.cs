namespace Backend.Models;

public class PokeApiResponse
{
    public int id { get; set; }
    public required string name { get; set; }
    public required Sprites sprites { get; set; }
}

public class Sprites
{
    public required string front_default { get; set; }
}
