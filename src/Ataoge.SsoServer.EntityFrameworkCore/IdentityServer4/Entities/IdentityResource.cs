using System;
using System.Collections.Generic;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class IdentityResource<TKey> : IEntity<TKey>
        where TKey :IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbField("Id", IsPrimaryKey = true)]
        public TKey Id { get; set; }

        [DbField("Enabled")]
        public bool Enabled { get; set; } = true;

        [DbField("Name")]
        public string Name { get; set; }

        [DbField("DisplayName")]
        public string DisplayName { get; set; }

        [DbField("Description")]
        public string Description { get; set; }

        [DbField("Required")]
        public bool Required { get; set; }

        [DbField("Emphasize")]
        public bool Emphasize { get; set; }

        [DbField("ShowInDiscoveryDocument")]
        public bool ShowInDiscoveryDocument { get; set; } = true;

        [DbField("Created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        
        [DbField("Updated")]
        public DateTime? Updated { get; set; }
        
        [DbField("NonEditable")]
        public bool NonEditable { get; set; }
        
    }

    [DbTable("IdentityResources")]
    public class IdentityResource : IdentityResource<int>
    {
        
        public List<IdentityClaim> UserClaims { get; set; }
        public List<IdentityResourceProperty> Properties { get; set; }
       
    }
}