namespace Patch.Data.Models;

public record Patch
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime DateTime { get; set; }
}
