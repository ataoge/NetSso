using System;
using Ataoge.Data;

namespace Microsoft.AspNetCore.Identity
{
    [DbTable("AspNetUsers")]
    public class EfIdentityUser<TKey> : IdentityUser<TKey>, IEntity<TKey>
      where TKey : IEquatable<TKey>
    {
        [DbField("Id", IsPrimaryKey = true)]
        public override TKey Id
        {
            get { return base.Id; }
            set { base.Id = value;}
        }

        public virtual string FamilyName {get; set;}

        public virtual string GivenName {get; set;}

        public virtual string GetDisplayName()
        {
            if (string.IsNullOrEmpty(this.GivenName) && string.IsNullOrEmpty(this.FamilyName))
                return this.UserName;
            return this.FamilyName + this.GivenName;
        }
    }
}