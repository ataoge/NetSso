using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    [DbTable("ApiSecrets")]
    public class ApiSecret<TKey> : Secret<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbRelationshipAttribute(Name = "FK_APISECRETS_RESOURCEID", FieldName = "ID", TableName = "APIRESOURCES", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ApiResourceId", IsForeignKey = true, NotNull = true)]
        public TKey ApiResourceId { get; set; }

       
    }

    public class ApiSecret : Secret
    {
        public int ApiResourceId { get; set; }
        public ApiResource ApiResource { get; set; }
    }
}