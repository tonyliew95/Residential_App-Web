using System;

//Tony Liew 20190313 RDT-118 \/
namespace Nop.Core.Domain.Residential.Incident
{
    public class Trx_Incident_File : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Inc_TypeId
        /// </summary>
        public int Inc_Id { get; set; }

        /// <summary>
        /// Gets or sets the Filename
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the File_URI
        /// </summary>
        public string File_URI { get; set; }

        /// <summary>
        /// Gets or sets the FileType
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }

    //Tony Liew 20190313 RDT-118 /\
}
