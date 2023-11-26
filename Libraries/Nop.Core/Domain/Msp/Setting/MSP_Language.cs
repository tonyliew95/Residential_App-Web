using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Enumeration;

namespace Nop.Core.Domain.Msp.Setting
{
    /// <summary>
    /// MSP_Language
    /// </summary>
    public partial class MSP_Language : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Language()
        {
           
        }

        /// <summary>
        /// Gets or sets the Language Code
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the Resource Name
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the Resource Value
        /// </summary>
        public string ResourceValue { get; set; }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets Created By 
        /// </summary>
        public string CreatedBy { get; set; }

    }
}