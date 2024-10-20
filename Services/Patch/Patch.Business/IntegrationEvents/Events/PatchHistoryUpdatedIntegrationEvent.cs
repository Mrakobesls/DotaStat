using EventBus.Events;

namespace Patch.Business.IntegrationEvents.Events;

public record PatchHistoryUpdatedIntegrationEvent(Patch[] Patches) : IntegrationEvent;

public record Patch(string Name, DateTime DateTime);
