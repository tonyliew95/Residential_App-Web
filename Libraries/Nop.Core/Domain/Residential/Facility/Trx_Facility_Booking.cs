using System;

//Tony Liew 20190417 RDT-202 \/
namespace Nop.Core.Domain.Residential.Facility
{
    public class Trx_Facility_Booking : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the FacilityId
        /// </summary>
        public int FacilityId { get; set; }

        /// <summary>
        /// Gets or Sets the BookingFrom
        /// </summary>
        public DateTime? BookingFrom { get; set; }

        /// <summary>
        /// Gets or Sets the BookingTo
        /// </summary>
        public DateTime? BookingTo { get; set; }

        /// <summary>
        /// Gets or Sets the OrganizationId
        /// </summary>
        public int? OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the PropertyId
        /// </summary>
        public int? PropertyId { get; set; }

        /// <summary>
        /// Gets or Sets the RequestBy
        /// </summary>
        public int? RequestBy { get; set; }

        /// <summary>
        /// Gets or Sets the ApprovedBy
        /// </summary>
        public int? ApprovedBy { get; set; }

        /// <summary>
        /// Gets or Sets the ApprovedOnUtc
        /// </summary>
        public DateTime? ApprovedOnUtc { get; set; }

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
//Tony Liew 20190417 RDT-202 /\