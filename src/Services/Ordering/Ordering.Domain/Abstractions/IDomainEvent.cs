using MediatR;

namespace Ordering.Domain.Abstractions;

/// <summary>
/// Marker interface for domain events. Domain events are notifications that something
/// important happened in the domain (for example: OrderPlaced, PaymentReceived).
/// This interface extends MediatR's <see cref="INotification"/> so events can be
/// published via MediatR and handled by subscribers.
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// A unique id for this event instance. Default implementation generates a new Guid.
    /// </summary>
    Guid EventId => Guid.NewGuid();

    /// <summary>
    /// When the event occurred. Default uses the current time when accessed.
    /// </summary>
    public DateTime OccurredOn => DateTime.Now;

    /// <summary>
    /// The CLR type of the event, useful when storing or routing events.
    /// </summary>
    public string EventType => GetType().AssemblyQualifiedName;
}
