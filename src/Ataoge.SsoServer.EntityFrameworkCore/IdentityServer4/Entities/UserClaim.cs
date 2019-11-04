using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    public abstract class UserClaim<TKey>: IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        //[DbAutoCreatedAttribute]
        [DbField("Id", IsPrimaryKey = true)]
        public virtual TKey Id { get; set; }

        [DbField("Type")]
        public virtual string Type { get; set; }
    }

    public abstract class UserClaim : UserClaim<int>
    {
        
    }
   
}