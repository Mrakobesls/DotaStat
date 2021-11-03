using DB_UnitOfWork.Inteface;
using DotaStat.Business.Interfaces;
using DotaStat.Business.Interfaces.Models;
using DotaStat.Business.Interfaces.Types;
using DotaStat.Data.EntityFramework.Model;
using System;
using System.Linq;

namespace DotaStat.Business
{
    public class HeroStatisticsService : GenericService, IHeroStatisticsService
    {
        private readonly IWeekPatchService _weekPatchService;
        private readonly IHeroService _heroService;

        public HeroStatisticsService(IUnitOfWork uow, IWeekPatchService weekPatchService, IHeroService heroService) : base(uow)
        {
            _weekPatchService = weekPatchService;
            _heroService = heroService;
        }

        #region Current Statistics

        public void AddPackResults(Pack pack)
        {
            EnsureDbArrayExisits();

            foreach (var heroMatchResultReadonly in pack.HeroWinСouples)
            {
                if (heroMatchResultReadonly is null) break;

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

                            var temp = _uow.HeroAllyWRs.Read(heroMatchResult.FirstHero, heroMatchResult.SecondHero);

                            if (heroMatchResult.MatchResult == MatchResult.Win)
                                temp.WinsOfMain++;
                            else
                                temp.LosesOfMain++;
                            _uow.HeroAllyWRs.Update(temp);

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

                            var temp = _uow.HeroEnemyWRs.Read(heroMatchResult.FirstHero, heroMatchResult.SecondHero);

                            if (heroMatchResult.MatchResult == MatchResult.Win)
                                temp.WinsOfMain++;
                            else
                                temp.LosesOfMain++;
                            _uow.HeroEnemyWRs.Update(temp);

                            break;
                        }
                }
            }

            _uow.SaveChanges();
        }
        public void EnsureDbArrayExisits()
        {
            _heroService.EnsureAllHeroesExists();
            var heroes = _heroService.GetAllHeroes();
            var heroesCount = heroes.Count;

            var estimatedWrRows = (heroesCount) / 2 * (heroesCount - 1);

            if (_uow.HeroEnemyWRs.ReadAll().Count() != estimatedWrRows)
            {
                _uow.HeroEnemyWRs.ResetTable();
                _uow.HeroAllyWRs.ResetTable();

                var currentWeekPatchId = _weekPatchService.GetCurrentWeekPatchId();
                for (byte i = 0; i < heroesCount - 1; i++)
                {
                    if (heroes[i].Id == 31)
                    {
                        Console.WriteLine();
                    }
                    for (byte j = (byte)(i + 1); j < heroesCount; j++)
                    {
                        var tempAlly = new CurrentWinrateAlly()
                        {
                            WeekPatchId = currentWeekPatchId,
                            MainHero = heroes[i].Id,
                            ComparedHero = heroes[j].Id
                        };
                        _uow.HeroAllyWRs.Create(tempAlly);
                        var tempEnemy = new CurrentWinrateEnemy()
                        {
                            WeekPatchId = currentWeekPatchId,
                            MainHero = heroes[i].Id,
                            ComparedHero = heroes[j].Id
                        };
                        _uow.HeroEnemyWRs.Create(tempEnemy);
                    }

                }

                _uow.SaveChanges();
            }
        }

        #endregion

        #region Weekly statistics

        public void Squash()
        {
            var heroes = _heroService.GetAllHeroes();
            var weekPatchId = _weekPatchService.GetCurrentWeekPatchId();

            for (int i = 1; i <= heroes.Count; i++)
            {
                var aggregatedWHW = new WeeklyWinrate();
                _uow.HeroEnemyWRs.ReadAll()
                    .Where(ewr => ewr.MainHero == heroes[i - 1].Id || ewr.ComparedHero == heroes[i - 1].Id)
                    .Select(hewr => new WeeklyWinrate()
                    {
                        HeroId = hewr.MainHero,
                        Wins = hewr.WinsOfMain,
                        AllGames = hewr.WinsOfMain + hewr.LosesOfMain
                    }).ToList().ForEach(weeklyHeroWinrate =>
                    {
                        if (weeklyHeroWinrate.HeroId != heroes[i - 1].Id)
                            weeklyHeroWinrate.Wins = weeklyHeroWinrate.AllGames - weeklyHeroWinrate.Wins;

                        aggregatedWHW.Wins += weeklyHeroWinrate.Wins;
                        aggregatedWHW.AllGames += weeklyHeroWinrate.AllGames;
                    });
                aggregatedWHW.HeroId = heroes[i - 1].Id;
                aggregatedWHW.WeekPatchId = weekPatchId;

                _uow.WeeklyWRs.Create(aggregatedWHW);
            }

            _uow.SaveChanges();
        }

        #endregion

    }
}