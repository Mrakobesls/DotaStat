using DB_UnitOfWork.Inteface;
using DotaStat.Business.Interfaces;
using DotaStat.Business.Interfaces.Types;

namespace DotaStat.Business
{
    public class CurrentHeroStatisticsService : GenericService, ICurrentHeroStatisticsService
    {
        public CurrentHeroStatisticsService(IUnitOfWork eow) : base(eow)
        {
        }

        public void AddMatchResult(Pack pack)
        {
            foreach (var heroMatchResult in pack.HeroWinrates)
            {
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
                            var temp = _eow.HeroAllyWRs.Read(heroMatchResult.FirstHero, heroMatchResult.SecondHero);
                            if (heroMatchResult.MatchResult == MatchResult.Win)
                                temp.WinsOfMain++;
                            else
                                temp.LosesOfMain++;
                            _eow.HeroAllyWRs.Update(temp);

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
                            var temp = _eow.HeroEnemyWRs.Read(heroMatchResult.FirstHero, heroMatchResult.SecondHero);
                            if (heroMatchResult.MatchResult == MatchResult.Win)
                                temp.WinsOfMain++;
                            else
                                temp.LosesOfMain++;
                            _eow.HeroEnemyWRs.Update(temp);

                            break;
                        }
                }

                _eow.SaveChanges();
            }
        }
        public int[,] ReadCurrentStatistics()
        {
            return new int[1, 1];
        }
    }
}
