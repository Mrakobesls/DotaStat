using EventBus.Events;

namespace Item.Business.IntegrationEvents.Events;

public record NewItemsReleasedIntegrationEvent(Item[] Heroes) : IntegrationEvent;

public record Item(int OriginalId, string Name);
