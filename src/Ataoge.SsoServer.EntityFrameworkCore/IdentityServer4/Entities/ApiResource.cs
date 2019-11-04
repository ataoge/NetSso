using System;
using System.Collections.Generic;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    [DbTable("ApiResources")]
    public class ApiResource<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
		[DbFieldAttribute("Id", IsPrimaryKey = true)]
        public TKey Id { get; set; }

        [DbFieldAttribute("Enabled")]
        public bool Enabled { get; set; } = true;

        [DbFieldAttribute("Name")]
        public string Name { get; set; }

        [DbFieldAttribute("DisplayName")]
        public string DisplayName { get; set; }

        [DbFieldAttribute("Description")]
        public string Description { get; set; }

        [DbFieldAttribute("Created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        
        [DbFieldAttribute("Updated")]
        public DateTime? Updated { get; set; }
        
        [DbFieldAttribute("LastAccessed")]
        public DateTime? LastAccessed { get; set; }
        
        [DbFieldAttribute("NonEditable")]
        public bool NonEditable { get; set; }
        
    }
    public class ApiResource : ApiResource<int>
    {
        
        public List<ApiSecret> Secrets { get; set; }
        public List<ApiScope> Scopes { get; set; }
        public List<ApiResourceClaim> UserClaims { get; set; }
        public List<ApiResourceProperty> Properties { get; set; }
        
    }
}