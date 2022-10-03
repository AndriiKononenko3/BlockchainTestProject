namespace BlockchainTestProject.Domain.Infrastructure.EventSourcing;

/// <summary>
/// Represents an identifiable entity that is event sourced.
/// </summary>
public interface IEventSourced
{
    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the collection of new events since the entity was loaded, as a consequence of command handling.
    /// </summary>
    IEnumerable<IEvent> Events { get; }
}