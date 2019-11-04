using System;
using Ataoge.Data;

namespace Microsoft.AspNetCore.Identity
{
    [DbTable("AspNetRoles")]
    public class EfIdentityRole<TKey> : IdentityRole<TKey>, ITreeEntity<TKey>, IEntity<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        [DbField("Id", IsPrimaryKey = true)]
        public override TKey Id
        {
            get { return base.Id; }
            set { base.Id = value;}
        }

        [DbField("Pid", IsParentKey = true)]
        public virtual TKey? Pid
        {
            get;
            set;
        }

        [DbField("DisplayName")]
		public virtual string DisplayName
		{
			get;
			set;
		}

        [DbField("RoleType", IsForeignKey = true)]
        public virtual int RoleType
        {
            get;
            set;
        }

		[DbField("SIndex", IsSortIndex = true)]
        public virtual int SIndex
        {
            get;
            set;
        }
    }

    [DbTable("AspNetRoles")]
    public class EfIdentityRole : IdentityRole<string>, ITreeEntity, IEntity<string>
    {
        [DbField("Id", IsPrimaryKey = true)]
        public override string Id
        {
            get { return base.Id; }
            set { base.Id = value;}
        }

        [DbField("Pid", IsParentKey = true)]
        public virtual string Pid
        {
            get;
            set;
        }

        [DbField("DisplayName")]
		public virtual string DisplayName
		{
			get;
			set;
		}

        [DbField("RoleType", IsForeignKey = true)]
        public virtual int RoleType
        {
            get;
            set;
        }

		[DbField("SIndex", IsSortIndex = true)]
        public virtual int SIndex
        {
            get;
            set;
        }

    }
}