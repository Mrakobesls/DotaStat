using EventBus.Events;

namespace IntegrationEventLogEF.Models;

public class IntegrationEventLogEntry
{
    private static readonly JsonSerializerOptions JsonIndentedOptions = new() { WriteIndented = true };
    private static readonly JsonSerializerOptions JsonCaseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };

    public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
    {
        EventId = @event.Id;
        CreationTime = @event.CreationDate;
        EventTypeName = @event.GetType().FullName;
        Content = JsonSerializer.Serialize(@event, @event.GetType(), JsonIndentedOptions);
        State = EventStateEnum.NotPublished;
        TimesSent = 0;
        TransactionId = transactionId.ToString();
    }
    public Guid EventId { get; private set; }
    public string EventTypeName { get; private set; }
    [NotMapped]
    public string EventTypeShortName => EventTypeName.Split('.')?.Last();
    [NotMapped]
    public IntegrationEvent IntegrationEvent { get; private set; }
    public EventStateEnum State { get; set; }
    public int TimesSent { get; set; }
    public DateTime CreationTime { get; private set; }
    public string Content { get; private set; }
    public string TransactionId { get; private set; }

    public IntegrationEventLogEntry DeserializeJsonContent(Type type)
    {
        IntegrationEvent = JsonSerializer.Deserialize(Content, type, JsonCaseInsensitiveOptions) as IntegrationEvent;

        return this;
    }
}
