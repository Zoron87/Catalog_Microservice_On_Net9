namespace Common.Messaging.Events;

public abstract class BaseIntegrationEvent
{
    public string CorrelationId { get; set; } = default!;
    public DateTime CreationDate { get; private set; }
    public string Eventtype { get; set; } = default!;

    protected BaseIntegrationEvent()
    {
        CorrelationId = Guid.NewGuid().ToString();
        CreationDate = DateTime.UtcNow;
        Eventtype = GetType().Name;
    }

    protected BaseIntegrationEvent(Guid correlationIn, DateTime creationDate)
    {
        CorrelationId = correlationIn.ToString();
        CreationDate = creationDate;
        Eventtype = GetType().Name;
    }
}
