using DotaStat.Business.Interfaces;
using DotaStat.Business.Interfaces.Models;
using DotaStat.Business.Interfaces.Types;
using DotaStat.SteamPoweredApiProvider;
using Quartz;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DotaStat.DataExtractor.Quartz
{
    class Fetcher : IJob
    {
        private readonly IHeroStatisticsService _heroStatisticsService;
        private readonly IWeekPatchService _weekPatchService;

        public Fetcher(IHeroStatisticsService heroStatisticsService, IWeekPatchService weekPatchService)
        {
            _heroStatisticsService = heroStatisticsService;
            _weekPatchService = weekPatchService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var lastNRankedMatches = SteamPoweredApi.GetLastNRankedMathces(); 
            if (lastNRankedMatches is null) return Task.CompletedTask;

            var pack = CreatePack(lastNRankedMatches);
            var weekPatchId = _weekPatchService.EnsureExisting(
                    _weekPatchService.GetNeededWeekId(lastNRankedMatches.Result.Matches.First().StartTime), 
                    _weekPatchService.GetCurrentPatch());

            _heroStatisticsService.AddPackResults(pack, weekPatchId);

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

                        if (firstPlayer.HeroId == 0 || secondPlayer.HeroId == 0)
                        {
                            Debug.WriteLine("Hero error" + firstPlayer.HeroId + " " + secondPlayer.HeroId);
                        }
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
