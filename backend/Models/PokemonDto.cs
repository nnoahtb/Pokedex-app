namespace Backend.Models;

public class PokemonDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Image { get; set; }
}
