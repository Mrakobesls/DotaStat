using EventBus.Abstractions;

namespace EventBus.Tests
{
    public class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public bool Handled { get; private set; }

        public Task Handle(TestIntegrationEvent @event)
        {
            Handled = true;
            return Task.CompletedTask;
        }
    }
}
