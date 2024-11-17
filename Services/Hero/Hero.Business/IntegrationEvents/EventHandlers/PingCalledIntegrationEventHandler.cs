using EventBus.Abstractions;
using Hero.Business.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;

namespace Hero.Business.IntegrationEvents.EventHandlers;

public class PingCalledIntegrationEventHandler(ILogger<PingCalledIntegrationEventHandler> logger)
    : IIntegrationEventHandler<PingCalledIntegrationEvent>
{
    public Task Handle(PingCalledIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        return Task.CompletedTask;
    }
}
