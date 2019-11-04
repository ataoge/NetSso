using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
 
    public class ApiScopeClaim<TKey> : UserClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbRelationshipAttribute(Name="FK_APISCOPECLAIMS_APISCOPEID", FieldName="ID", TableName = "APISCOPES", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ApiScopeId", IsForeignKey = true, NotNull = true)]
        public int ApiScopeId {get; set;}

        public ApiScope ApiScope { get; set; }
    }

    [DbTable("ApiScopeClaims")]
    public class ApiScopeClaim : UserClaim
    {
        public int ApiScopeId { get; set; }
        public ApiScope ApiScope { get; set; }
    }
}