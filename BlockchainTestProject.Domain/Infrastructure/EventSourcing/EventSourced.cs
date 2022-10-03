namespace BlockchainTestProject.Domain.Infrastructure.EventSourcing;

/// <summary>
/// Base class for event sourced entities that implements <see cref="IEventSourced"/>.
/// </summary>
/// <remarks>
/// <see cref="IEventSourced"/> entities do not require the use of <see cref="EventSourced"/>, but this class contains some common
/// useful functionality related to versions and rehydration from past events.
/// </remarks>
public abstract class EventSourced : IEventSourced
{
    private readonly Dictionary<Type, Action<IEvent>> handlers = new();

    private readonly List<IEvent> pendingEvents = new();

    private readonly Guid id;

    protected EventSourced(Guid id)
    {
        this.id = id;
    }

    public Guid Id => id;

    /// <summary>
    /// Gets the collection of new events since the entity was loaded, as a consequence of command handling.
    /// </summary>
    public IEnumerable<IEvent> Events => pendingEvents;

    /// <summary>
    /// Configures a handler for an event.
    /// </summary>
    protected void Handles<TEvent>(Action<TEvent> handler)
        where TEvent : IEvent
    {
        handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
    }

    protected void LoadFrom(IEnumerable<IEvent> pastEvents)
    {
        foreach (var e in pastEvents)
        {
            handlers[e.GetType()]
                .Invoke(e);
        }
    }

    protected void Update(Event e)
    {
        e.SourceId = Id;
        handlers[e.GetType()]
            .Invoke(e);
        pendingEvents.Add(e);
    }
}