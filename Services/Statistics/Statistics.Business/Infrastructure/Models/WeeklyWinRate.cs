using System.ComponentModel.DataAnnotations.Schema;

namespace Statistics.Business.Infrastructure.Models;

public class WeeklyWinRate
{
    public int WeekPatchId { get; set; }
    [ForeignKey(nameof(WeekPatchId))]
    public virtual WeekPatch WeekPatch { get; set; }
    public int HeroId { get; set; }
    [ForeignKey(nameof(HeroId))]
    public virtual Hero Hero { get; set; }
    public int Wins { get; set; }
    public int Loses { get; set; }
}
