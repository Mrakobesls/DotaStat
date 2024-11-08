using CommonSources.OpenDota;
using Newtonsoft.Json;

namespace Item.Business.Services;

public class OpenDotaHttpClient(HttpClient httpClient) : OpenDotaHttpClientBase(httpClient)
{
    private const string Items = "api/constants/item_ids";

    public async Task<Item[]> GetItems()
    {
        var patchHistoryResponse = await HttpClient.GetAsync(Items);
        var json = await patchHistoryResponse.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!
            .Select(x => new Item(int.Parse(x.Key), x.Value))
            .ToArray();
    }
}

public record Item(int Id, string Name);
