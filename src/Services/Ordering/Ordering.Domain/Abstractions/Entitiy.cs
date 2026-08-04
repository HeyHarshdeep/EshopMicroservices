using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    /// <summary>
    /// Base implementation of <see cref="Entity{T}"/> that provides
    /// a simple place to hold common entity properties.
    /// In DDD, most entities in the domain inherit from a base like this.
    /// </summary>
    /// <typeparam name="T">Type used for the entity Id.</typeparam>
    public abstract class Entitiy<T> : Entity<T>
    {
        // Unique identifier for this entity instance.
        public T Id { get; set; }

        // Audit properties. They are kept simple here; in a real app these
        // might be set automatically by infrastructure code when saving to a DB.
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
