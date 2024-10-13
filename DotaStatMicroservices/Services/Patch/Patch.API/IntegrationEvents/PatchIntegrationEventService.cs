using EventBus.Abstractions;
using EventBus.Events;

namespace Patch.API.IntegrationEvents;

public interface IPatchIntegrationEventService
{
    Task Publish(IntegrationEvent evt);
}

public class PatchIntegrationEventService(IEventBus eventBus, ILogger<PatchIntegrationEventService> logger) : IPatchIntegrationEventService
{
    public Task Publish(IntegrationEvent evt)
    {
        try
        {
            logger.LogInformation("Publishing integration event: {IntegrationEventId_published} - ({@IntegrationEvent})", evt.Id, evt);

            eventBus.Publish(evt);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", evt.Id, evt);
        }

        return Task.CompletedTask;
    }
}
