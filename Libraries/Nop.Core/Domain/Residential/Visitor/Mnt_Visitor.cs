using System;

namespace Nop.Core.Domain.Residential.Visitor
{
    public class Mnt_Visitor : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the OrganizationId
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the IcNo
        /// </summary>
        public string IcNo { get; set; }

        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the GenderId
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// Gets or Sets the CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or Sets the CountryId
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or Sets the Msisdn
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// Gets or Sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets the Image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or Sets the DriveIn
        /// </summary>
        public bool DriveIn { get; set; }

        /// <summary>
        /// Gets or Sets the VisitTypeId
        /// </summary>
        public int VisitTypeId { get; set; }

        /// <summary>
        /// Gets or Sets the VehicleId
        /// </summary>
        public int VehicleId { get; set; }

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
