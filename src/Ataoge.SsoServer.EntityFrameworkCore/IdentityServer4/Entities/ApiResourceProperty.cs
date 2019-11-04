using System;

namespace IdentityServer4.EntityFramework.Entities
{
    public class ApiResourceProperty<TKey> : Property<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey ApiResourceId { get; set; }
    }

    public class ApiResourceProperty : ApiResourceProperty<int>
    {
        public ApiResource ApiResource { get; set; }
    }
}