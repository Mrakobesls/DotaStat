using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Statistics.Business.IntegrationEvents.Events;
using Statistics.Business.Services;

namespace Statistics.Business.IntegrationEvents.EventHandlers;

public class NewItemsReleasedIntegrationEventHandler(IItemCommands itemCommands, ILogger<NewHeroesReleasedIntegrationEventHandler> logger)
    : IIntegrationEventHandler<NewHeroesReleasedIntegrationEvent>
{
    public async Task Handle(NewHeroesReleasedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        await itemCommands.AddRange(@event.Heroes.Select(x => new Types.Item { Id = x.OriginalId, Name = x.Name }));
    }
}
