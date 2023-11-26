using System;

namespace Nop.Core.Domain.Residential.Incident
{
    public class Mnt_Incident_Category : BaseEntity //wailiang 20190319 RDT-127
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public bool status { get; set; }

        /// <summary>
        /// Gets or sets the createdBy
        /// </summary>
        public int createdBy { get; set; }

        /// <summary>
        /// Gets or sets the createdOnUtc
        /// </summary>
        public DateTime createdOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the updatedBy
        /// </summary>
        public int updatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updatedOnUtc
        /// </summary>
        public DateTime updatedOnUtc { get; set; }
    }
}
