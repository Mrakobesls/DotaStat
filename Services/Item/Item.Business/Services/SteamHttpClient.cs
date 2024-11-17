using CommonSources.Steam;
using Item.Business.Types;
using Newtonsoft.Json;

namespace Item.Business.Services;

public class SteamHttpClient(HttpClient httpClient) : SteamHttpClientBase(httpClient)
{
    private const string GetItemsUri = "IEconDOTA2_570/GetGameItems/v1/?language=English";

    #region MatchesRequest

    public async Task<ItemsRequest> GetItems()
    {
        // var lastMatches = await _httpClient.GetFromJsonAsync<LastMatchesRequest?>(GetLast100MatchesUri + lastMatchSeqNum);
        var lastMatches = await HttpClient.GetAsync(GetItemsUri);
        var json = await lastMatches.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<ItemsRequest>(json)!;
    }

    #endregion
}
