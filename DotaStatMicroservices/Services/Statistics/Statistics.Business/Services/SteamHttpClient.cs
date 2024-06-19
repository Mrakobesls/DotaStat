using System.Diagnostics;
using Newtonsoft.Json;
using Statistics.Business.Types;

namespace Statistics.Business.Services
{
    public class SteamHttpClient
    {
        private const string GetLast100MatchesUri = "IDOTA2Match_570/GetMatchHistoryBySequenceNum/v001/?key=6130006785E38A13419F5D2C96475271&start_at_match_seq_num=";

        private const string GetLastMatchUri = "IDOTA2Match_570/GetMatchHistory/v001/?key=6130006785E38A13419F5D2C96475271&min_players=10&matches_requested=1";

        private const string GetHeroesUri = "IEconDOTA2_570/GetHeroes/v1/?key=6130006785E38A13419F5D2C96475271&language=English";

        private readonly HttpClient _httpClient;

        private string? _lastMatchSeqNum; //= "5154074033";//"4667818086";

        private string? LastMatchSeqNum
        {
            get
            {
                if (_lastMatchSeqNum is null)
                {
                    var lastMatch = GetLastMatchSeqNum();
                    lastMatch.Wait();

                    _lastMatchSeqNum = lastMatch.Result;
                }

                Console.WriteLine(_lastMatchSeqNum);
                Debug.WriteLine(_lastMatchSeqNum);

                return _lastMatchSeqNum;
            }
            set => _lastMatchSeqNum = value;
        }

        public SteamHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.steampowered.com");
        }

        #region MatchesRequest

        public async Task<LastMatchesRequest> GetLast100Matches(string lastMatchSeqNum)
        {
            // var lastMatches = await _httpClient.GetFromJsonAsync<LastMatchesRequest?>(GetLast100MatchesUri + lastMatchSeqNum);
            var lastMatches = await _httpClient.GetAsync(GetLast100MatchesUri + lastMatchSeqNum);
            var json = await lastMatches.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<LastMatchesRequest>(json)!;
        }

        public async Task<string> GetLastMatchSeqNum()
        {
            // var lastMatch = await _httpClient.GetFromJsonAsync<LastMatchesRequest>(GetLastMatchUri);
            var lastMatch = await _httpClient.GetAsync(GetLastMatchUri);
            var json = await lastMatch.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<LastMatchesRequest>(json)!.Result.Matches.First()
                .MatchSeqNum.ToString();
        }

        #endregion

        #region HeroesRequest

        public async Task<HeroesRequest> GetHeroes()
        {
            // return (await _httpClient.GetFromJsonAsync<HeroesRequest>(GetHeroesUri))!;
            var heroes = await _httpClient.GetAsync(GetHeroesUri);
            var json = await heroes.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<HeroesRequest>(json)!;
        }

        #endregion
    }
}
