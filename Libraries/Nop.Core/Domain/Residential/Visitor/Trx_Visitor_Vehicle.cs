using System;

namespace Nop.Core.Domain.Residential.Visitor
{
    public class Trx_Visitor_Vehicle : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the TrxVisitorId
        /// </summary>
        public int TrxVisitorId { get; set; }

        /// <summary>
        /// Gets or Sets the VehicleId
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or Sets the OrganizationId
        /// </summary>
        public int OrganizationId { get; set; }
        
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
