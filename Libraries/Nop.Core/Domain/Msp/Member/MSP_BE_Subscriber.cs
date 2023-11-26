using System;

namespace Nop.Core.Domain.Msp.Member
{
    /// <summary>
    /// Represents a subscriber
    /// </summary>
    public partial class MSP_BE_Subscriber : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_BE_Subscriber()
        {
        }

        /// <summary>
        /// Gets or sets the said
        /// </summary>
        public string said { get; set; }

        /// <summary>
        /// Gets or sets the SystemName
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the SystemDesc
        /// </summary>
        public string SystemDesc { get; set; }

        /// <summary>
        /// Gets or sets the BusinessNature
        /// </summary>
        public string BusinessNature { get; set; }

        /// <summary>
        /// Gets or sets the URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the OriginCountry_ID
        /// </summary>
        public string OriginCountry_ID { get; set; }

        /// <summary>
        /// Gets or sets the IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }
    }
}
