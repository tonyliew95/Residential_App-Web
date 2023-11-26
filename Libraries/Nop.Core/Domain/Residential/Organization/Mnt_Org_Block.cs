using System;

// WKK 20190322 RDT-163 [API] Login - profile dto
namespace Nop.Core.Domain.Residential.Organization
{
    public class Mnt_Org_Block : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the OrganizationId
        /// </summary>
        public int OrganizationId { get; set; }

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
        /// Gets or Sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }


    }
}
