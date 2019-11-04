using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class ClientProperty<TKey> : Property<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey ClientId {get; set;}
    }

    [DbTableAttribute("ClientProperties")]
    public class ClientProperty : ClientProperty<int>
    {
        //public override int ClientId { get; set; }
        public Client Client { get; set; }
    }
}