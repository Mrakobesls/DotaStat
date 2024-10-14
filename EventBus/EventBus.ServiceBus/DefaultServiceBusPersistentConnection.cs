namespace EventBus.ServiceBus;

public class DefaultServiceBusPersistentConnection : IServiceBusPersistentConnection
{
    private readonly string _serviceBusConnectionString;

    private ServiceBusClient _topicClient;

    private bool _disposed;

    public ServiceBusClient TopicClient
    {
        get
        {
            if (_topicClient.IsClosed)
            {
                _topicClient = new ServiceBusClient(_serviceBusConnectionString);
            }
            return _topicClient;
        }
    }

    public ServiceBusAdministrationClient AdministrationClient { get; }

    public DefaultServiceBusPersistentConnection(string serviceBusConnectionString)
    {
        _serviceBusConnectionString = serviceBusConnectionString;
        AdministrationClient = new ServiceBusAdministrationClient(_serviceBusConnectionString);
        _topicClient = new ServiceBusClient(_serviceBusConnectionString);
    }

    public ServiceBusClient CreateModel()
    {
        if (_topicClient.IsClosed)
        {
            _topicClient = new ServiceBusClient(_serviceBusConnectionString);
        }

        return _topicClient;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        _disposed = true;
        await _topicClient.DisposeAsync();
    }
}
