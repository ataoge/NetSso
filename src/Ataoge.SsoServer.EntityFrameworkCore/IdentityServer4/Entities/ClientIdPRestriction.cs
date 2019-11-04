using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class ClientIdPRestriction<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbFieldAttribute("Id", IsPrimaryKey = true)]
        public TKey Id { get; set; }

        [DbFieldAttribute("Provider")]
        public string Provider { get; set; }
        //public Client Client { get; set; }

        //[DbRelationshipAttribute(Name="FK_CLIENTIDPRESTRICTIONS_CLIENTID", FieldName="ID", TableName = "CLIENTS", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ClientId", IsForeignKey = true, NotNull = true)]
        public TKey ClientId {get; set;}
    }

    [DbTableAttribute("ClientIdPRestrictions")]
    public class ClientIdPRestriction : ClientIdPRestriction<int>
    {

        public Client Client { get; set; }
    }
}