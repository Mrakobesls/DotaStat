namespace EventBus.ServiceBus;

public interface IServiceBusPersistentConnection : IAsyncDisposable
{
    ServiceBusClient TopicClient { get; }
    ServiceBusAdministrationClient AdministrationClient { get; }
}