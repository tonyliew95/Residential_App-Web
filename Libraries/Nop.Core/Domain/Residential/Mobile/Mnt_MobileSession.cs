using System;

namespace Nop.Core.Domain.Residential.Mobile
{
    public class Mnt_MobileSession : BaseEntity
    {
        /// <summary>
        /// Gets or sets the CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the TokenId
        /// </summary>
        public string TokenId { get; set; }

        /// <summary>
        /// Gets or sets the DeviceUuid
        /// </summary>
        public string DeviceUuid { get; set; }

        /// <summary>
        /// Gets or sets the Valid
        /// </summary>
        public bool Valid { get; set; }

         /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOnUtc
        /// </summary>
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
