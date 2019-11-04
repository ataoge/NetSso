using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class ClientGrantType<TKey>: IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbFieldAttribute("Id", IsPrimaryKey = true)]
        public TKey Id { get; set; }

        [DbFieldAttribute("GrantType")]
        public string GrantType { get; set; }
        //public Client Client { get; set; }

        //[DbRelationshipAttribute(Name="FK_CLIENTGRANTTYPES_CLIENTID", FieldName="ID", TableName = "CLIENTS", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ClientId", IsForeignKey = true, NotNull = true)]
        public TKey ClientId {get; set;}
    }

    public class ClientGrantType : ClientGrantType<int>
    {
        public Client Client { get; set; }
    }
}