namespace Ordering.Domain.Abstractions;

/// <summary>
/// Generic interface that represents a domain entity with an identifier of type <typeparamref name="T"/>.
/// Use this to indicate any class that has an identity in the domain (for example: Order, Customer, Product).
/// </summary>
/// <typeparam name="T">Type of the entity identifier (Guid, int, string, etc.).</typeparam>
public interface Entity<T> : IEntity
{
    /// <summary>
    /// The unique identifier for the entity.
    /// As part of DDD an entity is primarily defined by its identity.
    /// </summary>
    public T Id { get; set; }
}


/// <summary>
/// Common properties shared by all entities in the domain.
/// These track basic audit information like who created or modified the entity and when.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// When the entity was created. Nullable to allow creation without immediate timestamping in tests.
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Who created the entity (could be a username, system id, etc.).
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// When the entity was last modified.
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Who last modified the entity.
    /// </summary>
    public string? LastModifiedBy { get; set; }
}
