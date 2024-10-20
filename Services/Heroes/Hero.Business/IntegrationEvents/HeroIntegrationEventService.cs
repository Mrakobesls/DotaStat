using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;

namespace Hero.Business.IntegrationEvents;

public class HeroIntegrationEventService(IEventBus eventBus, ILogger<HeroIntegrationEventService> logger) : IIntegrationEventService
{
    public Task Publish(IntegrationEvent @event)
    {
        try
        {
            logger.LogInformation("Publishing integration event: {IntegrationEventId_published} - ({@IntegrationEvent})", @event.Id, @event);

            eventBus.Publish(@event);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        }

        return Task.CompletedTask;
    }
}
