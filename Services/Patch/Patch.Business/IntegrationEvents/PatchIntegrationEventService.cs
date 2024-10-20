using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;

namespace Patch.Business.IntegrationEvents;

public class PatchIntegrationEventService(IEventBus eventBus, ILogger<PatchIntegrationEventService> logger) : IIntegrationEventService
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
