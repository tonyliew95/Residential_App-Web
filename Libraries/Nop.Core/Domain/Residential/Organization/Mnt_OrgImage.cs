using System;

// WKK 20190403 RDT-164 [API] Profile - Detail
namespace Nop.Core.Domain.Residential.Organization
{
    public class Mnt_OrgImage : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the Filename
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or Sets the File_URI
        /// </summary>
        public string File_URI { get; set; }

        /// <summary>
        /// Gets or Sets the FileType
        /// </summary>
        public string FileType { get; set; }

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
