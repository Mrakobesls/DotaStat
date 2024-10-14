using System.ComponentModel.DataAnnotations;

namespace Statistics.Business.Model;

public class UserRole
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<User> Users { get; set; }
}