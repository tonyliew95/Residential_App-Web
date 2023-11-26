using System;

namespace Nop.Core.Domain.Residential.Visitor
{
    public class Mnt_Visitor_Favourite : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the OrganizationId
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the PropertyId
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Gets or Sets the CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or Sets the VisitorId
        /// </summary>
        public int VisitorId { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
