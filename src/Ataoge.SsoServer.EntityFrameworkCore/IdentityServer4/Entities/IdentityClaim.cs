using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class IdentityClaim<TKey> : UserClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbRelationshipAttribute(Name="FK_IDENTITYCLAIMS_RESOURCEID", FieldName="ID", TableName = "IDENTITYRESOURCES", Type = RelationshipFlags.ManyToOne)]
        [DbField("IdentityResourceId", IsForeignKey = true, NotNull = true)]
        public virtual TKey IdentityResourceId { get; set; }
       
    }

    [DbTable("IdentityClaims")]
    public class IdentityClaim : IdentityClaim<int>
    {

        public IdentityResource IdentityResource { get; set; }
    }
}