using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    [DbTable("PersistedGrants")]
    public class PersistedGrant : IEntity
    {
		[DbField("Key", IsPrimaryKey = true)]
        public string Key { get; set; }
        
		[DbField("Type", IsPrimaryKey = true)]
        public string Type { get; set; }

        [DbField("SubjectId")]
        public string SubjectId { get; set; }

        [DbField("ClientId")]
        public string ClientId { get; set; }

        [DbField("CreationTime")]
        public DateTime CreationTime { get; set; }

        [DbField("Expiration")]
        public DateTime? Expiration { get; set; }

        [DbField("Data")]
        public string Data { get; set; }
    }
}

