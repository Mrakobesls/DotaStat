using CommonSources.Steam;
using Hero.Business.Types;
using Newtonsoft.Json;

namespace Hero.Business.Services;

public class SteamHttpClient(HttpClient httpClient) : SteamHttpClientBase(httpClient)
{
    private const string GetHeroesUri = "IEconDOTA2_570/GetHeroes/v1/?language=English";

    public async Task<HeroesRequest> GetHeroes()
    {
        // return (await _httpClient.GetFromJsonAsync<HeroesRequest>(GetHeroesUri))!;
        var heroes = await HttpClient.GetAsync(GetHeroesUri);
        var json = await heroes.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<HeroesRequest>(json)!;
    }
}
