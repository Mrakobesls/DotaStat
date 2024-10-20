using EventBus.Abstractions;
using Hero.Business.IntegrationEvents.Events;
using Hero.Business.Services;
using Microsoft.Extensions.Logging;

namespace Hero.Business.IntegrationEvents.EventHandlers;

public class PatchHistoryUpdatedIntegrationEventHandler(IHeroCommands heroCommands, ILogger<PatchHistoryUpdatedIntegrationEventHandler> logger)
    : IIntegrationEventHandler<PatchHistoryUpdatedIntegrationEvent>
{
    public Task Handle(PatchHistoryUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        heroCommands.EnsureAllExist();

        return Task.CompletedTask;
    }
}
