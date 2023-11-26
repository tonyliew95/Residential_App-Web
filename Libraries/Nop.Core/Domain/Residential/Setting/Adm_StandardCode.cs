using System;

namespace Nop.Core.Domain.Residential.Setting
{
    public class Adm_StandardCode : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or Sets the Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or Sets the Description
        /// </summary>
        public string Description { get; set; }

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
