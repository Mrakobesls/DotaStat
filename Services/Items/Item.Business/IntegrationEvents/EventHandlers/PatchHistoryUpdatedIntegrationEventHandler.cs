using EventBus.Abstractions;
using Item.Business.IntegrationEvents.Events;
using Item.Business.Services;
using Microsoft.Extensions.Logging;

namespace Item.Business.IntegrationEvents.EventHandlers;

public class PatchHistoryUpdatedIntegrationEventHandler(IItemCommands itemCommands, ILogger<PatchHistoryUpdatedIntegrationEventHandler> logger)
    : IIntegrationEventHandler<PatchHistoryUpdatedIntegrationEvent>
{
    public Task Handle(PatchHistoryUpdatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        itemCommands.EnsureAllExist();

        return Task.CompletedTask;
    }
}
