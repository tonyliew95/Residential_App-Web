using System;

// WKK 20190405 RDT-164 [API] Profile - Detail
namespace Nop.Core.Domain.Residential.Organization
{
    public class Mnt_OrgModuleSubscription : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the [OrganizationId]
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets the Announcement
        /// </summary>
        public string Announcement { get; set; }

        /// <summary>
        /// Gets or Sets the Incident
        /// </summary>
        public string Incident { get; set; }

        /// <summary>
        /// Gets or Sets the Facility
        /// </summary>
        public string Facility { get; set; }

        /// <summary>
        /// Gets or Sets the FamilyTenant
        /// </summary>
        public string FamilyTenant { get; set; }

        /// <summary>
        /// Gets or Sets the Intercom
        /// </summary>
        public string Intercom { get; set; }

        /// <summary>
        /// Gets or Sets the Visitor
        /// </summary>
        public string Visitor { get; set; }

        /// <summary>
        /// Gets or Sets the Community
        /// </summary>
        public string Community { get; set; }

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
