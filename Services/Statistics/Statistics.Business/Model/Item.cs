using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Statistics.Business.Model;

[Table(nameof(Item))]
public class Item
{
    [Key]
    public short Id { get; set; }
    public string Name { get; set; }
}