using CommonSources.Steam;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PatchChecker.Business.Services;

public class OpenDotaHttpClient(HttpClient httpClient) : SteamHttpClientBase(httpClient)
{
    private const string NamedPatchNotes = "constants/patchnotes";
    private const string PatchHistory = "constants/patch";

    public async Task<IEnumerable<string>> GetNamedPatches()
    {
        var patchNotes = await HttpClient.GetAsync(NamedPatchNotes);
        var json = await patchNotes.Content.ReadAsStringAsync();
        var jObject = JObject.Parse(json);
        var patchNames = jObject.Properties()
            .Select(x => x.Name);

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

public record PatchHistory(string Name, DateTime DateTime);
