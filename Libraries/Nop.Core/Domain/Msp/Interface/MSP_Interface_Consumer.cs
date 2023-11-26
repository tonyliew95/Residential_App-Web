using System;

namespace Nop.Core.Domain.Msp.Interface
{
    public partial class MSP_Interface_Consumer : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Interface_Consumer()
        {
        }

        /// <summary>
        /// Gets or sets the GlobalUserID
        /// </summary>
        public string GlobalUserID { get; set; }

        /// <summary>
        /// Gets or sets the said
        /// </summary>
        public string said { get; set; }

        /// <summary>
        /// Gets or sets the CustomerID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

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

        /// <summary>
        /// Gets or sets the IsProceed
        /// </summary>
        public bool IsProceed { get; set; }
    }
}
