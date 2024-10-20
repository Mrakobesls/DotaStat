using EventBus.Abstractions;
using Patch.Business.IntegrationEvents;
using Patch.Business.IntegrationEvents.Events;
using Patch.Data.Repository;
using Patch.Data.Types;

namespace Patch.Business.Services;

public interface IPatchService
{
    Task EnsureLatestPatch();
    Task EnsurePatchHistory();
}

public class PatchService : IPatchService
{
    private readonly OpenDotaHttpClient _openDotaHttpClient;
    private readonly IDataPatchCommands _dataPatchCommands;
    private readonly IDataPatchQueries _dataPatchQueries;
    private readonly IIntegrationEventService _integrationEventService;

    public PatchService(
        OpenDotaHttpClient openDotaHttpClient,
        IDataPatchCommands dataPatchCommands,
        IDataPatchQueries dataPatchQueries,
        IIntegrationEventService integrationEventService
    )
    {
        _openDotaHttpClient = openDotaHttpClient;
        _dataPatchCommands = dataPatchCommands;
        _dataPatchQueries = dataPatchQueries;
        _integrationEventService = integrationEventService;
    }

    public async Task EnsureLatestPatch()
    {
        var latestPatch = await _openDotaHttpClient.GetLastNamedPatch();
        var currentLocalPatch = await _dataPatchQueries.GetCurrentPatch();

        if (latestPatch == currentLocalPatch)
        {
            return;
        }

        await _dataPatchCommands.AddNewPatch(
            new PatchCreate
            {
                Name = latestPatch,
                DateTime = DateTime.UtcNow
            }
        );
        await _integrationEventService.Publish(
            new PatchHistoryUpdatedIntegrationEvent([new IntegrationEvents.Events.Patch(latestPatch, DateTime.UtcNow)])
        );
    }

    public async Task EnsurePatchHistory()
    {
        var namedPatches = await _openDotaHttpClient.GetNamedPatches(); // with letter
        var currentPatch = await _dataPatchQueries.GetCurrentPatch();
        // already done initial work before
        if (namedPatches.Last() == currentPatch)
        {
            return;
        }

        var patchHistory = await _openDotaHttpClient.GetPatchHistory(); // without letter

        var fullPatchHistory = patchHistory.Select(
                x =>
                {
                    var patches = namedPatches.Where(y => y.StartsWith(x.Name))
                        .Select(
                            y => new PatchCreate
                            {
                                Name = y,
                                DateTime = x.DateTime
                            }
                        )
                        .ToArray();
                    return patches.Any()
                        ? patches
                        :
                        [
                            new PatchCreate
                            {
                                Name = x.Name,
                                DateTime = x.DateTime
                            }
                        ];
                }
            )
            .SelectMany(x => x);

        var localPatches = await _dataPatchQueries.GetPatches();
        var patchHistoryDifference = fullPatchHistory.ExceptBy(
                localPatches.Select(x => x.Name),
                x => x.Name
            )
            .ToArray();

        await _dataPatchCommands.AddPatchHistory(patchHistoryDifference);

        await _integrationEventService.Publish(
            new PatchHistoryUpdatedIntegrationEvent(
                patchHistoryDifference.Select(x => new IntegrationEvents.Events.Patch(x.Name, x.DateTime))
                    .ToArray()
            )
        );
    }
}
