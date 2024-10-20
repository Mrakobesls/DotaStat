using Patch.Data.Repository;

namespace Patch.Business.Services;

public interface IPatchQueries
{
    Task<string> GetCurrentPatch();
}

public class PatchQueries(IDataPatchQueries dataPatchQueries) : IPatchQueries
{
    public async Task<string> GetCurrentPatch()
    {
        return await dataPatchQueries.GetCurrentPatch();
    }
}
