using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;

namespace Item.Business.IntegrationEvents;

public class ItemIntegrationEventService(IEventBus eventBus, ILogger<ItemIntegrationEventService> logger) : IIntegrationEventService
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
