using Patch.Data.Repository;

namespace Patch.Business.Services;

public interface IPatchQueries
{
    Task<string> GetCurrentPatch();
}

public class PatchQueries : IPatchQueries
{
    private IDataPatchQueries _dataPatchQueries;

    public PatchQueries(IDataPatchQueries dataPatchQueries)
    {
        _dataPatchQueries = dataPatchQueries;
    }

    public async Task<string> GetCurrentPatch()
    {
        return await _dataPatchQueries.GetCurrentPatch();
    }
}
