using Patch.Business.Services;
using Quartz;

namespace Patch.Observer.Jobs;

public class EnsureLastPatchJob : IJob
{
    private readonly IPatchService _patchService;

    public EnsureLastPatchJob(IPatchService patchService)
    {
        _patchService = patchService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _patchService.EnsureLatestPatch();
    }
}
