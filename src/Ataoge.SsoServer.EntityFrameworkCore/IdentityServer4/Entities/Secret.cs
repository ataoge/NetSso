using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public abstract class Secret<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual string Description { get; set; }
        public virtual string Value { get; set; }
        public virtual DateTime? Expiration { get; set; }
        public virtual string Type { get; set; } = "SharedSecret";
        public virtual DateTime Created { get; set; } = DateTime.UtcNow;
    }

    public abstract class Secret : Secret<int>
    {
        
    }
}