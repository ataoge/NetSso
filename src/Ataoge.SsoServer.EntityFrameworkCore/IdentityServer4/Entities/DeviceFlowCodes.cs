using System;
using Ataoge.Data;

namespace IdentityServer4.EntityFramework.Entities
{
    /// <summary>
    /// Entity for device flow codes
    /// </summary>
    [DbTable("DeviceFlowCodes")]
    public class DeviceFlowCodes : IEntity
    {
        /// <summary>
        /// Gets or sets the device code.
        /// </summary>
        /// <value>
        /// The device code.
        /// </value>
        [DbField("DeviceCode", IsPrimaryKey = true)]
        public string DeviceCode { get; set; }

        /// <summary>
        /// Gets or sets the user code.
        /// </summary>
        /// <value>
        /// The user code.
        /// </value>
        [DbField("UserCode", IsPrimaryKey = true)]
        public string UserCode { get; set; }

        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>
        /// The subject identifier.
        /// </value>
        [DbField("SubjectId")]
        public string SubjectId { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [DbField("ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>
        /// The creation time.
        /// </value>
        [DbField("CreationTime")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        /// <value>
        /// The expiration.
        /// </value>
        [DbField("Expiration")]
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DbField("Data")]
        public string Data { get; set; }
    }
}