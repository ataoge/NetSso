using System;

namespace IdentityServer4.EntityFramework.Entities
{
    public class IdentityResourceProperty<TKey> : Property<TKey>
        where TKey :IEquatable<TKey>
    {
        public virtual TKey IdentityResourceId { get; set; }
        //public IdentityResource<TKey> IdentityResource { get; set; }
    }

    public class IdentityResourceProperty : IdentityResourceProperty<int>
    {
        public IdentityResource IdentityResource { get; set; }
    }
}