using EventBus.Events;

namespace EventBus.Abstractions;

public interface IIntegrationEventService
{
    Task Publish(IntegrationEvent @event);
}
