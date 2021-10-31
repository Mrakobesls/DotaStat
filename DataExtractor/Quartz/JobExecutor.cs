using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotaStat.Business.Interfaces;
using DotaStat.Business.Interfaces.Types;
using DotaStat.DataExtractor.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace DotaStat.DataExtractor.Quartz
{
    class JobExecutor : IJob
    {
        private static readonly string _getLast100Matches =
            "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v001/?key=6FA22370F0F8EAE0B42BB466DF82CE1F&start_at_match_seq_num=";
        private static readonly string _getLastMatch =
                    "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v001/?key=6FA22370F0F8EAE0B42BB466DF82CE1F&min_players=10&game_mode=7";

        private readonly ICurrentHeroStatisticsService _currentHeroStatisticsService;
        private static string LastMatchSeqNum { get; set; }

        static JobExecutor()
        {
            var request = (HttpWebRequest)WebRequest.Create(_getLastMatch);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using var response = (HttpWebResponse)request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);

            var json = reader.ReadToEnd();
            var lastMatch = JsonConvert.DeserializeObject<LastMatchesRequest>(json);

            LastMatchSeqNum = lastMatch.Result.Matches.First().MatchSeqNum.ToString();
        }

        public JobExecutor(ICurrentHeroStatisticsService currentHeroStatisticsService)
        {
            _currentHeroStatisticsService = currentHeroStatisticsService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var last99Matches = GetLast99Maches();
            var pack = CreatePack(last99Matches);
            _currentHeroStatisticsService.AddMatchResult(pack);

            var num = last99Matches.Result.Matches.Count(x => x.LobbyType == 7);

            return Task.CompletedTask;
        }

        public LastMatchesRequest GetLast99Maches()
        {
            var request = (HttpWebRequest)WebRequest.Create(_getLast100Matches + LastMatchSeqNum);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using var response = (HttpWebResponse)request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            var json = reader.ReadToEnd();

            var last100Matches = JsonConvert.DeserializeObject<LastMatchesRequest>(json);
            LastMatchSeqNum = last100Matches.Result.Matches[^1].MatchSeqNum.ToString();

            last100Matches.Result.Matches = last100Matches.Result.Matches[1..^1];

            return last100Matches;
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

                        pack.HeroWinrates[counter++] = new HeroMatchResult()
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
