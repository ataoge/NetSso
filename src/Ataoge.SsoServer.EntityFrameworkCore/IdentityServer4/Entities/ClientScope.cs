using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class ClientScope<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbFieldAttribute("Id", IsPrimaryKey = true)]
        public TKey Id { get; set; }

        [DbFieldAttribute("Scope")]
        public string Scope { get; set; }
        //public Client Client { get; set; }
        
        //[DbRelationshipAttribute(Name="FK_CLIENTSCOPES_CLIENTID", FieldName="ID", TableName = "CLIENTS", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ClientId", IsForeignKey = true, NotNull = true)]
        public TKey ClientId {get; set;}
    }

    [DbTableAttribute("ClientScopes")]
    public class ClientScope : ClientScope<int>
    {
        public Client Client { get; set; }
    }
}