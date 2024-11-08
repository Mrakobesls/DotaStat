using System.ComponentModel.DataAnnotations;

namespace Statistics.Business.Infrastructure.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
