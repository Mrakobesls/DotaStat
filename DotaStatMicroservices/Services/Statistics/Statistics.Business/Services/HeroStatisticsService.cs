using Microsoft.EntityFrameworkCore;
using Statistics.Business.Enums;
using Statistics.Business.Model;
using Statistics.Business.Types;

namespace Statistics.Business.Services
{
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

            await _dbContext.CurrentWinrateAllies.LoadAsync();
            await _dbContext.CurrentWinrateEnemies.LoadAsync();
            foreach (var heroMatchResultReadonly in pack.HeroWinCouples)
            {
                var heroMatchResult = heroMatchResultReadonly;
                switch (heroMatchResult.HeroRelations)
                {
                    case HeroRelations.Allies:
                    {
                        if (heroMatchResult.FirstHero > heroMatchResult.SecondHero)
                        {
                            var tempp = heroMatchResult.FirstHero;
                            heroMatchResult.FirstHero = heroMatchResult.SecondHero;
                            heroMatchResult.SecondHero = tempp;
                        }

                        var temp = _dbContext.CurrentWinrateAllies.First(
                            x => x.MainHeroId == heroMatchResult.FirstHero
                                && x.ComparedHeroId == heroMatchResult.SecondHero
                        );

                        if (heroMatchResult.MatchResult == MatchResult.Win)
                            temp.Wins++;
                        else
                            temp.Loses++;
                        _dbContext.CurrentWinrateAllies.Update(temp);

                        break;
                    }
                    case HeroRelations.Enemies:
                    {
                        if (heroMatchResult.FirstHero > heroMatchResult.SecondHero)
                        {
                            var tempp = heroMatchResult.FirstHero;
                            heroMatchResult.FirstHero = heroMatchResult.SecondHero;
                            heroMatchResult.SecondHero = tempp;
                            heroMatchResult.MatchResult = heroMatchResult.MatchResult == MatchResult.Win
                                ? MatchResult.Lose
                                : MatchResult.Win;
                        }

                        var temp = _dbContext.CurrentWinrateEnemies.First(
                            x => x.MainHeroId == heroMatchResult.FirstHero
                                && x.ComparedHeroId == heroMatchResult.SecondHero
                        );

                        if (heroMatchResult.MatchResult == MatchResult.Win)
                            temp.Wins++;
                        else
                            temp.Loses++;
                        _dbContext.CurrentWinrateEnemies.Update(temp);

                        break;
                    }
                    default:
                        throw new Exception($"Invalid relationship {heroMatchResult.HeroRelations}");
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task EnsureDbArrayExists(int weekPatchId)
        {
            await _heroService.EnsureAllExist();
            var heroes = _heroService.GetAll();
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
                        var tempAlly = new CurrentWinRateAlly
                        {
                            WeekPatchId = weekPatchId,
                            MainHeroId = heroes[i].Id,
                            ComparedHeroId = heroes[j].Id
                        };
                        _dbContext.CurrentWinrateAllies.Add(tempAlly);
                        var tempEnemy = new CurrentWinRateEnemy
                        {
                            WeekPatchId = weekPatchId,
                            MainHeroId = heroes[i].Id,
                            ComparedHeroId = heroes[j].Id
                        };
                        _dbContext.CurrentWinrateEnemies.Add(tempEnemy);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Weekly statistics

        public async Task Squash(int tabletWeekPatch)
        {
            var heroes = _heroService.GetAll();

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
}
