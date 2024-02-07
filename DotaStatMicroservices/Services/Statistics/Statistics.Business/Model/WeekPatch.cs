using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotaStat.Data.EntityFramework.Model
{
    public class WeekPatch 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual ICollection<WeeklyWinrate> WeeklyHeroWinRates { get; set; }
        public virtual ICollection<CurrentWinrateAlly> LastWeekHeroAlliesWinRates { get; set; }
        public virtual ICollection<CurrentWinrateEnemy> LastWeekHeroEnemiesWinRates { get; set; }
        public int WeekId { get; set; }
        public string Patch { get; set; }
    }
}
