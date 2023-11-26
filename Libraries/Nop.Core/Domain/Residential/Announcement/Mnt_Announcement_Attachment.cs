using System;

//WKK 20190315 RDT-121 \/
namespace Nop.Core.Domain.Residential.Announcement
{
    public class Mnt_Announcement_Attachment : BaseEntity
    {
        /// <summary>
        /// Gets or sets the AnnouncementId
        /// </summary>
        public int AnnouncementId { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public bool Type { get; set; }

        /// <summary>
        /// Gets or sets the Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the MimeType
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the FileSize
        /// </summary>
        public string FileSize { get; set; }

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

    //WKK 20190315 RDT-121 /\
}
