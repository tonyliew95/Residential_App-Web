using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Core.Domain.Msp.Setting
{
    public partial class MSP_Consumption_Platform : BaseEntity
    {
        public MSP_Consumption_Platform()
        {
        }

        /// <summary>
        /// Gets or sets the Platform ID
        /// </summary>
        public int PlatformID { get; set; }

        /// <summary>
        /// Gets or sets the PlatformCode
        /// </summary>
        public string PlatformCode { get; set; } //WilliamHo 20181010 

        /// <summary>
        /// Gets or sets the Platform Name
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Tooltips
        /// </summary>
        public string Tooltips { get; set; }

        /// <summary>
        /// Gets or sets the IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the ExpiredOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }
    }
}
