using System;

//WKK 20190315 RDT-121
namespace Nop.Core.Domain.Residential.Announcement
{
    public class Mnt_Announcement : BaseEntity
    {
        public Mnt_Announcement() { }

        /// <summary>
        /// Gets or sets the Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the PublishedOnUtc
        /// </summary>
        public DateTime PublishedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the ExpiredOnUtc
        /// </summary>
        public DateTime ExpiredOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the OrganizationId
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
