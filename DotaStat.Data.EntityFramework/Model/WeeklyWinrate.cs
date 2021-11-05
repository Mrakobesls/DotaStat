using System.ComponentModel.DataAnnotations.Schema;

namespace DotaStat.Data.EntityFramework.Model
{
    public class WeeklyWinrate
    {
        public int WeekPatchId { get; set; }
        [ForeignKey("WeekPatchId")]
        public virtual WeekPatch WeekPatch { get; set; }
        public byte HeroId { get; set; }
        [ForeignKey("HeroId")]
        public virtual Hero Hero { get; set; }
        public int Wins { get; set; }
        public int AllGames { get; set; }
    }
}
