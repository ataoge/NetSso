using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public abstract class Property<TKey> : IEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
    }

    public abstract class Property : Property<int>
    {
        
    }
}