using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    
    public class ClientSecret<TKey> : Secret<TKey>
        where  TKey : IEquatable<TKey>
    {
        //[DbRelationshipAttribute(Name="FK_CLIENTSCERETS_CLIENTID", FieldName="ID", TableName = "CLIENTS", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ClientId", IsForeignKey = true, NotNull = true)]
        public TKey ClientId { get; set; }
        //public Client Client { get; set; }
    }

    [DbTableAttribute("ClientSecrets")]
    public class ClientSecret : ClientSecret<int>
    {
        public Client Client { get; set; }
    }
}