using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Residential.Setting
{
    // Tony Liew 20190320 \/
    public class Adm_SystemControl : BaseEntity
    {
        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or Sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets the Active
        /// </summary>
        public bool Active { get; set; }

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
// Tony Liew 20190320 /\
