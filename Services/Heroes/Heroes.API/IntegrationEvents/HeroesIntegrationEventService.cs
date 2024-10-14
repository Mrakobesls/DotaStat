using EventBus.Abstractions;
using EventBus.Events;

namespace Heroes.API.IntegrationEvents;

public interface IHeroesIntegrationEventService
{
    Task Publish(IntegrationEvent evt);
}

public class HeroesIntegrationEventService(IEventBus eventBus, ILogger<HeroesIntegrationEventService> logger) : IHeroesIntegrationEventService
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
