using System;

namespace Nop.Core.Domain.Residential.Setting
{
    public class Adm_TimeZone : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the IdName
        /// </summary>
        public string IdName { get; set; }

        /// <summary>
        /// Gets or Sets the StandardName
        /// </summary>
        public string StandardName { get; set; }

        /// <summary>
        /// Gets or Sets the DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or Sets the UtcOffset
        /// </summary>
        public string UtcOffset { get; set; }

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
