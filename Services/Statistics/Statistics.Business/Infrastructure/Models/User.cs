using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Statistics.Business.Infrastructure.Models;

public class User
{
    [Key]
    public long SteamId { get; set; }
    public DateTime MyProperty { get; set; }

    public int Role { get; set; }
    [ForeignKey(nameof(Role))]
    public virtual UserRole Roles { get; set; }

    public bool IsDeleted { get; set; }
}