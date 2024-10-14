using EventBus.Abstractions;

namespace EventBus.Tests
{
    public class TestIntegrationOtherEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public bool Handled { get; private set; }

        public Task Handle(TestIntegrationEvent @event)
        {
            Handled = true;
            return Task.CompletedTask;
        }
    }
}
