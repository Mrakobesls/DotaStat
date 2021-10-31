using System.ComponentModel.DataAnnotations.Schema;
using DataBase.Model;

namespace DotaStat.Data.EntityFramework.Model
{
    public class CurrentWinrateEnemy
    {
        public int WeekPatchId { get; set; }
        [ForeignKey("WeekPatchId")]
        public virtual WeekPatch WeekPatch { get; set; }

        public byte MainHero { get; set; }
        //[ForeignKey("MainHero")]
        public virtual Hero HeroMain { get; set; }

        public byte ComparedHero { get; set; }
        //[ForeignKey("ComparedHero")]
        public virtual Hero HeroCompareWith { get; set; }

        public int WinsOfMain { get; set; }
        public int LosesOfMain { get; set; }
    }
}
