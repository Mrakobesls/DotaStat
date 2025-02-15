﻿namespace Statistics.Business.Infrastructure.Models;

public class Hero
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<CurrentWinRateAlly> CurrentWinRateAlliesMains { get; set; }
    public ICollection<CurrentWinRateAlly> CurrentWinRateAlliesCompareds { get; set; }
    public ICollection<CurrentWinRateEnemy> CurrentWinRateEnemiesMains { get; set; }
    public ICollection<CurrentWinRateEnemy> CurrentWinRateEnemiesCompareds { get; set; }
    public ICollection<WeeklyWinRate> WeeklyWinRates { get; set; }
}
