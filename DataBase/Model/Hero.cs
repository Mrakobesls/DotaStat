using System.Collections.Generic;

namespace DataBase.Model
{
    public class Hero
    {
        public byte Id { get; set; }
        public ICollection<LastWeekHeroWinRate> LastWeekHeroWinRates { get; set; }
        //public ICollection<LastWeekHeroWinRate> LastWeekHeroCompared { get; set; }
        public ICollection<WeeklyHeroWinRate> WeeklyHeroWinRates { get; set; }
        public string Name { get; set; }
    }
}
