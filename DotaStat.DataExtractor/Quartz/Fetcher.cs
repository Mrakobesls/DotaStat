using DotaStat.Business.Interfaces;
using DotaStat.Business.Interfaces.Models;
using DotaStat.Business.Interfaces.Types;
using DotaStat.SteamPoweredApiProvider;
using Quartz;
using System;
using System.Threading.Tasks;

namespace DotaStat.DataExtractor.Quartz
{
    class Fetcher : IJob
    {
        private readonly IHeroStatisticsService _heroStatisticsService;

        public Fetcher(IHeroStatisticsService heroStatisticsService)
        {
            _heroStatisticsService = heroStatisticsService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var lastNRankedMatches = SteamPoweredApi.GetLastNRankedMathces(); 
            if (lastNRankedMatches is null) return Task.CompletedTask;

            var pack = CreatePack(lastNRankedMatches);
            _heroStatisticsService.AddPackResults(pack);

            return Task.CompletedTask;
        }

        public Pack CreatePack(LastMatchesRequest lastMatchesRequest)
        {
            var pack = new Pack();
            int counter = 0;
            foreach (var match in lastMatchesRequest.Result.Matches)
            {
                for (int i = 0; i < match.Players.Length; i++)
                {
                    var firstPlayer = match.Players[i];

                    for (int j = i + 1; j < match.Players.Length; j++)
                    {
                        var secondPlayer = match.Players[j];

                        var heroRelations = IsAllies(firstPlayer.PlayerSlot, secondPlayer.PlayerSlot) 
                            ? HeroRelations.Allies 
                            : HeroRelations.Enemies;
                        var isFirstHeroWin = match.RadiantWin ^ IsRadiant(firstPlayer.PlayerSlot)
                            ? MatchResult.Lose
                            : MatchResult.Win;
                        
                        pack.HeroWinСouples[counter++] = new HeroMatchResult()
                        {
                            FirstHero = firstPlayer.HeroId,
                            SecondHero = secondPlayer.HeroId,
                            HeroRelations = heroRelations,
                            MatchResult = isFirstHeroWin
                        };
                    }
                }
            }

            return pack;
        }

        public bool IsAllies(int firstHeroSlot, int secondHeroSlot)
        {
            return Math.Abs(firstHeroSlot - secondHeroSlot) < 5;
        }

        public bool IsRadiant(int heroSlot)
        {
            return heroSlot < 5;
        }


    }
}
