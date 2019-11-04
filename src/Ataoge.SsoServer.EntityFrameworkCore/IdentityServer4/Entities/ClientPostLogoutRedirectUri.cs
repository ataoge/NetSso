using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class ClientPostLogoutRedirectUri<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbFieldAttribute("Id", IsPrimaryKey = true)]
        public TKey Id { get; set; }

        [DbFieldAttribute("PostLogoutRedirectUri")]
        public string PostLogoutRedirectUri { get; set; }
        //public Client Client { get; set; }
        
        //[DbRelationshipAttribute(Name="FK_CLIENTCORSORIGINS_CLIENTID", FieldName="ID", TableName = "CLIENTS", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ClientId", IsForeignKey = true, NotNull = true)]
        public TKey ClientId {get; set;}
    }

     [DbTableAttribute("ClientPostLogoutRedirectUris")]
    public class ClientPostLogoutRedirectUri : ClientPostLogoutRedirectUri<int>
    {
      
        public Client Client { get; set; }
    }
}