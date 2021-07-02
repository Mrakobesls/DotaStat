using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Model
{
    public class LastWeekHeroWinRate
    {
        public int WeekPatchId { get; set; }
        [ForeignKey("WeekPatchId")]
        public virtual WeekPatch WeekPatch { get; set; }
        public byte HeroIdMain { get; set; }
        [ForeignKey("HeroIdMain")]
        public virtual Hero HeroMain { get; set; }
        public byte HeroIdCompareWith { get; set; }
        //[ForeignKey("HeroIdCompareWith")]
        //public virtual Hero HeroCompareWith { get; set; }
        public int WinAlly { get; set; }
        public int LoseAlly { get; set; }
        public int WinEnemy { get; set; }
        public int WinLose { get; set; }
    }
}
