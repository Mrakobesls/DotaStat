using System;
using System.IO;
using System.Linq;
using System.Net;
using DotaStat.Business.Interfaces.Models;
using Newtonsoft.Json;

namespace DotaStat.SteamPoweredApiProvider
{
    public static class SteamPoweredApi
    {
        private static readonly string _getLast100Matches =
            "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v001/?key=6FA22370F0F8EAE0B42BB466DF82CE1F&start_at_match_seq_num=";
        private static readonly string _getLastMatch =
            "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v001/?key=6FA22370F0F8EAE0B42BB466DF82CE1F&min_players=10&game_mode=7&matches_requested=1";
        private static readonly string _getHeroes =
            "http://api.steampowered.com/IEconDOTA2_570/GetHeroes/v1/?key=6FA22370F0F8EAE0B42BB466DF82CE1F&language=English";

        private static string _lastMatchSeqNum;
        private static string LastMatchSeqNum
        {
            get
            {
                if (_lastMatchSeqNum is null)
                    SetLastMatchSeqNum();
                return _lastMatchSeqNum;
            }
            set => _lastMatchSeqNum = value;
        }

        #region TemplateRequest
        private static T HttpWebRequestTemple<T>(string url) where T : class
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using var response = (HttpWebResponse)request.GetResponse();
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                var json = reader.ReadToEnd();

                var answer = JsonConvert.DeserializeObject<T>(json);
                return answer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        #endregion

        #region MatchesRequest
        public static LastMatchesRequest GetLastNRankedMathces()
        {
            LastMatchesRequest lastNMatches = HttpWebRequestTemple<LastMatchesRequest>(_getLast100Matches + LastMatchSeqNum);
            if (lastNMatches is null) return null;

            LastMatchSeqNum = lastNMatches.Result.Matches[^1].MatchSeqNum.ToString();

            lastNMatches.Result.Matches = lastNMatches.Result.Matches[1..^1].Where(x => x.LobbyType == 7).ToArray();

            return lastNMatches;
        }

        public static void SetLastMatchSeqNum()
        {
            LastMatchesRequest lastNMatches = HttpWebRequestTemple<LastMatchesRequest>(_getLastMatch);
            LastMatchSeqNum = lastNMatches?.Result.Matches.First().MatchSeqNum.ToString();
        }

        #endregion

        #region HeroesRequest
        public static HeroesRequest GetHeroes()
        {
            return HttpWebRequestTemple<HeroesRequest>(_getHeroes);
        }

        #endregion
    }
}
