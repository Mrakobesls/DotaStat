using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Statistics.Business.Model;

[Table(nameof(WeekPatch))]
public class WeekPatch 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int WeekId { get; set; }
    public string Patch { get; set; }

    public virtual ICollection<WeeklyWinRate> WeeklyHeroWinRates { get; set; }
    public virtual ICollection<CurrentWinRateAlly> LastWeekHeroAlliesWinRates { get; set; }
    public virtual ICollection<CurrentWinRateEnemy> LastWeekHeroEnemiesWinRates { get; set; }
}