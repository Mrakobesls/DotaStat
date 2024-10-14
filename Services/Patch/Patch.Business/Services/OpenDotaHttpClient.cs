using CommonSources.OpenDota;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Patch.Business.Services;

public class OpenDotaHttpClient(HttpClient httpClient) : OpenDotaHttpClientBase(httpClient)
{
    private const string NamedPatchNotes = "api/constants/patchnotes";
    private const string PatchHistory = "api/constants/patch";

    public async Task<IEnumerable<string>> GetNamedPatches()
    {
        var patchNotes = await HttpClient.GetAsync(NamedPatchNotes);
        var json = await patchNotes.Content.ReadAsStringAsync();
        var jObject = JObject.Parse(json);
        var patchNames = jObject.Properties()
            .Select(x => x.Name.Replace("_", "."))
            .ToArray();

        return patchNames;
    }

    public async Task<string> GetLastNamedPatch()
    {
        return (await GetNamedPatches()).Last();
    }

    public async Task<IEnumerable<PatchHistory>> GetPatchHistory()
    {
        var patchHistoryResponse = await HttpClient.GetAsync(PatchHistory);
        var json = await patchHistoryResponse.Content.ReadAsStringAsync();

        var patchHistory = JsonConvert.DeserializeObject<PatchHistory[]>(json)!;

        return patchHistory;
    }
}

public record PatchHistory(string Name, [JsonProperty("date")] DateTime DateTime);
