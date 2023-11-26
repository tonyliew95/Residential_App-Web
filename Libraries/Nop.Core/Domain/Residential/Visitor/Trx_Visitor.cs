using System;

namespace Nop.Core.Domain.Residential.Visitor
{
    public class Trx_Visitor : BaseEntity
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
        /// Gets or Sets the VisitorId
        /// </summary>
        public int VisitorId { get; set; }

        /// <summary>
        /// Gets or Sets the VisitTypeId
        /// </summary>
        public int VisitTypeId { get; set; }

        /// <summary>
        /// Gets or Sets the ResidentId
        /// </summary>
        public int ResidentId { get; set; }

        /// <summary>
        /// Gets or Sets the ResidentUnit
        /// </summary>
        public string ResidentUnit { get; set; }

        /// <summary>
        /// Gets or Sets the VisitingDate
        /// </summary>
        public DateTime VisitingDate { get; set; }

        /// <summary>
        /// Gets or Sets the TotalVisitor
        /// </summary>
        public int TotalVisitor { get; set; }

        /// <summary>
        /// Gets or Sets the DriveIn
        /// </summary>
        public bool DriveIn { get; set; }

        /// <summary>
        /// Gets or Sets the Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
        
        /// <summary>
        /// Gets or Sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }
        
        /// <summary>
        /// Gets or Sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
        
    }
}
