using System;

// Tony Liew 20190315 RDT-65 \/
namespace Nop.Core.Domain.Residential.Organization
{
    public class Mnt_Organization : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the TypeId
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or Sets the ApplicationId
        /// </summary>
        public int? ApplicationId { get; set; }

        /// <summary>
        /// Gets or Sets the CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or Sets the Msisdn
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// Gets or Sets the Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or Sets the TotalBlock
        /// </summary>
        public Int16? TotalBlock { get; set; }

        /// <summary>
        /// Gets or Sets the TotalUnit
        /// </summary>
        public Int16? TotalUnit { get; set; }

        /// <summary>
        /// Gets or Sets the FacilityService
        /// </summary>
        public bool? FacilityService { get; set; }

        /// <summary>
        /// Gets or Sets the OrgImageId
        /// </summary>
        public int OrgImageId { get; set; }

        /// <summary>
        /// Gets or Sets the TimeZoneId
        /// </summary>
        public int TimeZoneId { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }


    }
}
// Tony Liew 20190315 RDT-65 /\