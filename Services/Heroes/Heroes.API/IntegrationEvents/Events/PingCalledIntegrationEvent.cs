using EventBus.Events;

namespace Heroes.API.IntegrationEvents.Events;

public record PingCalledIntegrationEvent(string Data) : IntegrationEvent;
