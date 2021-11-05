using System.Collections.Generic;

namespace DotaStat.Data.EntityFramework.Model
{
    public class Hero
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<CurrentWinrateAlly> CurrentWinrateAlliesMain { get; set; }
        public ICollection<CurrentWinrateAlly> CurrentWinrateAlliesCompared { get; set; }
        public ICollection<CurrentWinrateEnemy> CurrentWinrateEnemiesMain { get; set; }
        public ICollection<CurrentWinrateEnemy> CurrentWinrateEnemiesCompared { get; set; }
        public ICollection<WeeklyWinrate> WeeklyHeroWinRates { get; set; }
    }
}
