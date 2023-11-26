using System;

namespace Nop.Core.Domain.Residential.Visitor
{
    public class Mnt_Visitor_Vehicle : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the VisitorId
        /// </summary>
        public int VisitorId { get; set; }

        /// <summary>
        /// Gets or Sets the V_PlatNumber
        /// </summary>
        public string V_PlatNumber { get; set; }

        /// <summary>
        /// Gets or Sets the VehicleType
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Gets or Sets the V_Model
        /// </summary>
        public string V_Model { get; set; }

        /// <summary>
        /// Gets or Sets the V_Variant
        /// </summary>
        public string V_Variant { get; set; }

        /// <summary>
        /// Gets or Sets the Status
        /// </summary>
        public bool Status { get; set; }

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
