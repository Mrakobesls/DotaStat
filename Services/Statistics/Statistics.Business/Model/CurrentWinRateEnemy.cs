using System.ComponentModel.DataAnnotations.Schema;

namespace Statistics.Business.Model;

[Table(nameof(CurrentWinRateEnemy))]
public class CurrentWinRateEnemy
{
    public int WeekPatchId { get; set; }
    [ForeignKey(nameof(WeekPatchId))]
    public virtual WeekPatch WeekPatch { get; set; }

    public int MainHeroId { get; set; }
    //[ForeignKey("MainHero")]
    public virtual Hero MainHero { get; set; }

    public int ComparedHeroId { get; set; }
    //[ForeignKey("ComparedHero")]
    public virtual Hero ComparedHero { get; set; }

    public int Wins { get; set; }
    public int Loses { get; set; }
}