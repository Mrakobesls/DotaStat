using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotaStat.Data.EntityFramework.Model
{
    public class User
    {
        [Key]
        public long SteamId { get; set; }
        public DateTime MyProperty { get; set; }

        public int Role { get; set; }
        [ForeignKey("Role")]
        public virtual UserRole UserRole { get; set; }

        public bool IsDeleted { get; set; }
    }
}
