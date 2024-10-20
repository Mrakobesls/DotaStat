using CommonSources.OpenDota;
using Newtonsoft.Json;

namespace Item.Business.Services;

public class OpenDotaHttpClient(HttpClient httpClient) : OpenDotaHttpClientBase(httpClient)
{
    private const string Items = "api/constants/item_ids";

    public async Task<Items> GetItems()
    {
        var patchHistoryResponse = await HttpClient.GetAsync(Items);
        var json = await patchHistoryResponse.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Items>(json)!;
    }
}

public record Items((int Id, string Name)[] Data);
