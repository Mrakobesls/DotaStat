using Xunit;

namespace EventBus.Tests
{
    public class InMemorySubscriptionManagerTests
    {
        [Fact]
        public void AfterCreation_ShouldBeEmpty()
        {
            var manager = new InMemoryEventBusSubscriptionsManager();

            Assert.True(manager.IsEmpty);
        }

        [Fact]
        public void AddedOneEventSubscription_ContainTheEvent()
        {
            var manager = new InMemoryEventBusSubscriptionsManager();
            manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

            Assert.True(manager.HasSubscriptionsForEvent<TestIntegrationEvent>());
        }

        [Fact]
        public void AllSubscriptionsAreDeleted_EventNoLongerExists()
        {
            var manager = new InMemoryEventBusSubscriptionsManager();

            manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            manager.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

            Assert.False(manager.HasSubscriptionsForEvent<TestIntegrationEvent>());
        }

        [Fact]
        public void DeletingSubscription_RaisedOnEventRemoved()
        {
            var raised = false;
            var manager = new InMemoryEventBusSubscriptionsManager();

            manager.OnEventRemoved += (o, e) => raised = true;
            manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            manager.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();

            Assert.True(raised);
        }

        [Fact]
        public void GetHandlersForEvent_ReturnAllHandlers()
        {
            var manager = new InMemoryEventBusSubscriptionsManager();

            manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            manager.AddSubscription<TestIntegrationEvent, TestIntegrationOtherEventHandler>();
            var handlers = manager.GetHandlersForEvent<TestIntegrationEvent>();

            Assert.Equal(2, handlers.Count());
        }

    }
}
