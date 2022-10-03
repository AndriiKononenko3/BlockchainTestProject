namespace BlockchainTestProject.Domain.Infrastructure.EventSourcing;

public abstract class Event : IEvent
{
    public Guid SourceId { get; set; }
}