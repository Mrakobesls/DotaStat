using System.ComponentModel.DataAnnotations;

namespace DotaStat.Data.EntityFramework.Model
{
    public class Item
    {
        [Key]
        public short Id { get; set; }
        public string Name { get; set; }
    }
}
