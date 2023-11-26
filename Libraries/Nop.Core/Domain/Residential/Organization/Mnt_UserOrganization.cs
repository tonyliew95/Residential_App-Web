using System;

// Tony Liew 20190315 RDT-65 \/
namespace Nop.Core.Domain.Residential.Organization
{
    public class Mnt_UserOrganization : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the Organization_Id
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the BatchPropId
        /// </summary>
        public int? BatchPropId { get; set; }

        /// <summary>
        /// Gets or Sets the Customer_Id
        /// </summary>
        //public int CustomerId { get; set; } //WKK 20190410 RDT-168 [API] Property Unit - Bind new property
        public int? CustomerId { get; set; } //WKK 20190410 RDT-168 [API] Property Unit - Bind new property

        /// <summary>
        /// Gets or Sets the Reid
        /// </summary>
        public string Reid { get; set; }

        /// <summary>
        /// Gets or Sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets the Country Code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or Sets the Msisdn
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// Gets or Sets the Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or Sets the ActivationCode
        /// </summary>
        public string ActivationCode { get; set; }

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
// Tony Liew 20190315 RDT-65 /\
