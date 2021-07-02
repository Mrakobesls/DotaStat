using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Model
{
    public class WeekPatch 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual ICollection<WeeklyHeroWinRate> WeeklyHeroWinRates { get; set; }
        public virtual ICollection<LastWeekHeroWinRate> LastWeekHeroWinRates { get; set; }
        public int WeekId { get; set; }
        public string Patch { get; set; }
    }
}
