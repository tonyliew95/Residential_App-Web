using System;

namespace Nop.Core.Domain.Residential.User
{
    public class Mnt_UserAccount : BaseEntity
    {
        public Mnt_UserAccount() { }

        /// <summary>
        /// Gets or sets the [UserOrgId]
        /// </summary>
        public int UserOrgId { get; set; }

        /// <summary>
        /// Gets or sets the [CustomerId]
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the [AccountType]
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or sets the [Occupancy]
        /// </summary>
        public string Occupancy { get; set; }

        /// <summary>
        /// Gets or sets the [Reid]
        /// </summary>
        public string Reid { get; set; }

        /// <summary>
        /// Gets or sets the [ActivationCode]
        /// </summary>
        public string ActivationCode { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
