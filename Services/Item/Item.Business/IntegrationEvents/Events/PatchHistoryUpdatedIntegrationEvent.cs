using EventBus.Events;

namespace Item.Business.IntegrationEvents.Events;

public record PatchHistoryUpdatedIntegrationEvent(Patch[] Patches) : IntegrationEvent;

public record Patch(string Name, DateTime DateTime);
