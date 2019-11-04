using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{

    public class ClientClaim<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbFieldAttribute("Id", IsPrimaryKey = true)]
        public TKey Id { get; set; }

        [DbFieldAttribute("Type")]
        public string Type { get; set; }

         [DbFieldAttribute("Value")]
        public string Value { get; set; }
        //public Client Client { get; set; }

        //[DbRelationshipAttribute(Name="FK_CLIENTCLAIMS_CLIENTID", FieldName="ID", TableName = "CLIENTS", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ClientId", IsForeignKey = true, NotNull = true)]
        public TKey ClientId {get; set;}
    }

    public class ClientClaim : ClientClaim<int>
    {
        public Client Client { get; set; }
    }
}