using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Model
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public string Name { get; set; }
    }
}
