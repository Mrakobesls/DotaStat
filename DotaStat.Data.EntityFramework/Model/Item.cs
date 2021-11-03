using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Model
{
    public class Item
    {
        [Key]
        public short Id { get; set; }
        public string Name { get; set; }
    }
}
