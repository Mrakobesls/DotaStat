using PatchChecker.Data.Repository;

namespace PatchChecker.Business.Services;

public class PatchQueries
{
    private OpenDotaHttpClient _openDotaHttpClient;
    private DataPatchQueries _dataPatchQueries;

    public PatchQueries(OpenDotaHttpClient openDotaHttpClient, DataPatchQueries dataPatchQueries)
    {
        _openDotaHttpClient = openDotaHttpClient;
        _dataPatchQueries = dataPatchQueries;
    }

    public async Task<string> GetCurrentPatch()
    {
        return await _dataPatchQueries.GetCurrentPatch();
    }
}
