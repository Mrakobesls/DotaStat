using Microsoft.EntityFrameworkCore;
using Statistics.Business.Enums;
using Statistics.Business.Model;
using Statistics.Business.Types;

namespace Statistics.Business.Services;

public interface IHeroStatisticsService
{
    Task AddPackResults(Pack pack, int weekPatchId);
    Task Squash(int tabletWeekPatch);
    List<WeeklyWinRate> GetHeroWrHistory(int heroId);
}

public class HeroStatisticsService : IHeroStatisticsService
{
    private readonly IWeekPatchService _weekPatchService;
    private readonly IHeroService _heroService;
    private readonly DotaStatDbContext _dbContext;

    public HeroStatisticsService(IWeekPatchService weekPatchService, IHeroService heroService, DotaStatDbContext dbContext)
    {
            _weekPatchService = weekPatchService;
            _heroService = heroService;
            _dbContext = dbContext;
        }

    #region Current Statistics

    public async Task AddPackResults(Pack pack, int weekPatchId)
    {
            await EnsureDbArrayExists(weekPatchId);

            var currentWinrateAllies = await _dbContext.CurrentWinrateAllies.ToListAsync();
            var currentWinrateEnemies = await _dbContext.CurrentWinrateEnemies.ToListAsync();

            foreach (var heroMatchResultReadonly in pack.HeroWinCouples)
            {
                var heroMatchResult = heroMatchResultReadonly;
                switch (heroMatchResult.HeroRelations)
                {
                    case HeroRelations.Allies:
                    {
                        if (heroMatchResult.FirstHero > heroMatchResult.SecondHero)
                        {
                            (heroMatchResult.FirstHero, heroMatchResult.SecondHero) = (heroMatchResult.SecondHero, heroMatchResult.FirstHero);
                        }

                        var temp = currentWinrateAllies.First(
                            x => x.MainHeroId == heroMatchResult.FirstHero
                                && x.ComparedHeroId == heroMatchResult.SecondHero
                        );

                        if (heroMatchResult.MatchResult == MatchResult.Win)
                        {
                            temp.Wins++;
                        }
                        else
                        {
                            temp.Loses++;
                        }

                        break;
                    }
                    case HeroRelations.Enemies:
                    {
                        if (heroMatchResult.FirstHero > heroMatchResult.SecondHero)
                        {
                            (heroMatchResult.FirstHero, heroMatchResult.SecondHero) = (heroMatchResult.SecondHero, heroMatchResult.FirstHero);
                            heroMatchResult.MatchResult = heroMatchResult.MatchResult == MatchResult.Win
                                ? MatchResult.Lose
                                : MatchResult.Win;
                        }

                        var temp = currentWinrateEnemies.First(
                            x => x.MainHeroId == heroMatchResult.FirstHero
                                && x.ComparedHeroId == heroMatchResult.SecondHero
                        );

                        if (heroMatchResult.MatchResult == MatchResult.Win)
                        {
                            temp.Wins++;
                        }
                        else
                        {
                            temp.Loses++;
                        }

                        break;
                    }
                    default:
                        throw new Exception($"Invalid relationship {heroMatchResult.HeroRelations}");
                }
            }
            _dbContext.CurrentWinrateAllies.UpdateRange(currentWinrateAllies);
            _dbContext.CurrentWinrateEnemies.UpdateRange(currentWinrateEnemies);

            await _dbContext.SaveChangesAsync();
        }

    private async Task EnsureDbArrayExists(int weekPatchId)
    {
            await _heroService.EnsureAllExist();
            var heroes = await _heroService.GetAllAsync();
            var heroesCount = heroes.Count;

            var estimatedWrRows = (heroesCount) / 2 * (heroesCount - 1);

            var tabletWeekPatch = (_dbContext.CurrentWinrateEnemies.FirstOrDefault()?.WeekPatchId ?? -1);

            if (weekPatchId != tabletWeekPatch /* || _uow.HeroEnemyWRs.ReadAll().Count() != estimatedWrRows*/)
            {
                if (tabletWeekPatch != -1)
                {
                    await Squash(tabletWeekPatch);
                }

                for (var i = 0; i < heroesCount - 1; i++)
                {
                    for (var j = i + 1; j < heroesCount; j++)
                    {
                        _dbContext.CurrentWinrateAllies.Add(new CurrentWinRateAlly
                        {
                            WeekPatchId = weekPatchId,
                            MainHeroId = heroes[i].Id,
                            ComparedHeroId = heroes[j].Id
                        });
                        _dbContext.CurrentWinrateEnemies.Add(new CurrentWinRateEnemy
                        {
                            WeekPatchId = weekPatchId,
                            MainHeroId = heroes[i].Id,
                            ComparedHeroId = heroes[j].Id
                        });
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
        }

    #endregion

    #region Weekly statistics

    public async Task Squash(int tabletWeekPatch)
    {
            var heroes = await _heroService.GetAllAsync();

            for (var i = 1; i <= heroes.Count; i++)
            {
                var aggregatedWhw = new WeeklyWinRate();
                _dbContext.CurrentWinrateEnemies.Where(
                        ewr => ewr.MainHeroId == heroes[i - 1].Id
                            || ewr.ComparedHeroId == heroes[i - 1].Id
                    )
                    .Select(
                        hewr => new WeeklyWinRate
                        {
                            HeroId = hewr.MainHeroId,
                            Wins = hewr.Wins,
                            Loses = hewr.Wins + hewr.Loses
                        }
                    )
                    .ToList()
                    .ForEach(
                        weeklyHeroWinrate =>
                        {
                            if (weeklyHeroWinrate.HeroId != heroes[i - 1].Id)
                                weeklyHeroWinrate.Wins = weeklyHeroWinrate.Loses - weeklyHeroWinrate.Wins;

                            aggregatedWhw.Wins += weeklyHeroWinrate.Wins;
                            aggregatedWhw.Loses += weeklyHeroWinrate.Loses;
                        }
                    );
                aggregatedWhw.HeroId = heroes[i - 1].Id;
                aggregatedWhw.WeekPatchId = tabletWeekPatch;
                aggregatedWhw.Wins /= 5;
                aggregatedWhw.Loses /= 5;

                _dbContext.WeeklyHeroWinRates.Add(aggregatedWhw);
            }

            await _dbContext.SaveChangesAsync();

            _dbContext.CurrentWinrateEnemies.Truncate();
            _dbContext.CurrentWinrateAllies.Truncate();
            // _dbContext.DetachAllEntities();

           await  _dbContext.SaveChangesAsync();
        }

    #endregion

    #region GetStatistics

    public List<WeeklyWinRate> GetHeroWrHistory(int heroId)
    {
            return _dbContext.WeeklyHeroWinRates
                .Where(h => h.HeroId == heroId)
                .OrderBy(s => s.WeekPatch.WeekId)
                .ThenBy(s2 => s2.WeekPatch.Patch)
                .ToList();
        }

    #endregion
}