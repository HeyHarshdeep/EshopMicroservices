namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// Base class for aggregate roots. Provides a simple mechanism to record domain events
    /// produced by the aggregate and to expose them for publishing by infrastructure.
    /// </summary>
    /// <typeparam name="TId">Type of the aggregate id.</typeparam>
    public abstract class Aggregate<TId> : Entitiy<TId>, IAggregate<TId>
    {
        // Internal list that accumulates domain events produced by this aggregate.
        private readonly List<IDomainEvent> _domainEvents = new();

        // Expose events in a read-only fashion so outside code can't modify the list directly.
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Add a domain event to the aggregate's internal queue.
        /// Call this from domain logic when something noteworthy happens.
        /// </summary>
        /// <param name="domainEvent">Event instance to record.</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Return and clear all domain events. Typically infrastructure will call this
        /// after saving changes and then publish the returned events.
        /// </summary>
        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] dequeueEvents = _domainEvents.ToArray();

            // Clear internal list so events aren't re-published accidentally.
            _domainEvents.Clear();

            return dequeueEvents;
        }


    }
}
