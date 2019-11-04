using System;
using System.Collections.Generic;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class ApiScope<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbFieldAttribute("Id", IsPrimaryKey = true)]
        public TKey Id { get; set; }

        [DbFieldAttribute("Name")]
        public string Name { get; set; }

        [DbFieldAttribute("DisplayName")]
        public string DisplayName { get; set; }

        [DbFieldAttribute("Description")]
        public string Description { get; set; }

        [DbFieldAttribute("Required")]
        public bool Required { get; set; }

        [DbFieldAttribute("Emphasize")]
        public bool Emphasize { get; set; }

        [DbFieldAttribute("ShowInDiscoveryDocument")]
        public bool ShowInDiscoveryDocument { get; set; } = true;

        //[DbRelationshipAttribute(Name="FK_APISCOPES_RESOURCEID", FieldName="ID", TableName = "APIRESOURCES", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ApiResourceId", IsForeignKey = true, NotNull = true)]
        public TKey ApiResourceId {get; set;}
    }

    public class ApiScope : ApiScope<int>
    {
       
        public List<ApiScopeClaim> UserClaims { get; set; }

        
        public ApiResource ApiResource { get; set; }
    }
}