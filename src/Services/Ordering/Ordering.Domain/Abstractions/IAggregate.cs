namespace Ordering.Domain.Abstractions;


/// <summary>
/// Marker interface for aggregate roots with a typed Id.
/// An aggregate is a cluster of domain objects that can be treated as a single unit
/// (for example: Order and its OrderItems). Aggregate roots are the only objects
/// that external code should hold references to or load from repositories.
/// </summary>
public interface IAggregate<T> : IAggregate, Entity<T>
{

}

/// <summary>
/// Non-generic parts of an aggregate root contract.
/// Provides access to domain events produced by the aggregate and a method to clear them.
/// Aggregates accumulate domain events which infrastructure can publish after a transaction.
/// </summary>
public interface IAggregate : IEntity
{
    /// <summary>
    /// Read-only list of domain events that have been raised by this aggregate.
    /// Handlers typically read these and act (persisting or publishing) after the aggregate is saved.
    /// </summary>
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Dequeue and return all accumulated domain events. The aggregate should remove
    /// events from its internal list so they are not published more than once.
    /// </summary>
    IDomainEvent[] CleadDomainEvents();
}
