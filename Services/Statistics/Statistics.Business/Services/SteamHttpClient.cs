using CommonSources.Steam;
using Newtonsoft.Json;
using Statistics.Business.Types;

namespace Statistics.Business.Services;

public class SteamHttpClient(HttpClient httpClient) : SteamHttpClientBase(httpClient)
{
    private const string Get100MatchesUri = "IDOTA2Match_570/GetMatchHistoryBySequenceNum/v001/?start_at_match_seq_num=";

    private const string GetLastMatchUri = "IDOTA2Match_570/GetMatchHistory/v001/?min_players=10&matches_requested=1";

    private const string GetHeroesUri = "IEconDOTA2_570/GetHeroes/v1/?language=English";

    #region MatchesRequest

    public async Task<LastMatchesRequest> GetNext100Matches(string lastMatchSeqNum)
    {
        // var lastMatches = await _httpClient.GetFromJsonAsync<LastMatchesRequest?>(GetLast100MatchesUri + lastMatchSeqNum);
        var lastMatches = await HttpClient.GetAsync(Get100MatchesUri + lastMatchSeqNum);
        var json = await lastMatches.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<LastMatchesRequest>(json)!;
    }

    public async Task<string> GetLastMatchSeqNum()
    {
        // var lastMatch = await _httpClient.GetFromJsonAsync<LastMatchesRequest>(GetLastMatchUri);
        var lastMatch = await HttpClient.GetAsync(GetLastMatchUri);
        var json = await lastMatch.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<LastMatchesRequest>(json)!.Result.Matches.First()
            .MatchSeqNum.ToString();
    }

    #endregion

    #region HeroesRequest

    public async Task<HeroesRequest> GetHeroes()
    {
        // return (await _httpClient.GetFromJsonAsync<HeroesRequest>(GetHeroesUri))!;
        var heroes = await HttpClient.GetAsync(GetHeroesUri);
        var json = await heroes.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<HeroesRequest>(json)!;
    }

    #endregion
}
