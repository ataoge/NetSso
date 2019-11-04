using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public class ApiResourceClaim<TKey> : UserClaim<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbRelationshipAttribute(Name="FK_APICLAIMS_RESOURCEID", FieldName="ID", TableName = "APIRESOURCES", Type = RelationshipFlags.ManyToOne)]
        [DbFieldAttribute("ApiResourceId", IsForeignKey = true, NotNull = true)]
        public int ApiResourceId {get; set;}

    }

    [DbTable("ApiClaims")]
    public class ApiResourceClaim : ApiResourceClaim<int>
    {
        
        public ApiResource ApiResource { get; set; }
    }
}