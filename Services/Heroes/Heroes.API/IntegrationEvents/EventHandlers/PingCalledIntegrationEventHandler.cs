using EventBus.Abstractions;
using Heroes.API.IntegrationEvents.Events;

namespace Heroes.API.IntegrationEvents.EventHandlers;

public class PingCalledIntegrationEventHandler(ILogger<PingCalledIntegrationEventHandler> logger)
    : IIntegrationEventHandler<PingCalledIntegrationEvent>
{
    public Task Handle(PingCalledIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        return Task.CompletedTask;
    }
}
