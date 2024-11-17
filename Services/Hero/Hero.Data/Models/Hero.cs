namespace Hero.Data.Models;

public record Hero
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
