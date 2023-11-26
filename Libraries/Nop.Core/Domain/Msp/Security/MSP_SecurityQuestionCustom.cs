using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Enumeration;

namespace Nop.Core.Domain.Msp.Security
{
    /// <summary>
    /// Represents a Session
    /// </summary>
    public partial class MSP_SecurityQuestionCustom : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_SecurityQuestionCustom()
        {
           
        }

        /// <summary>
        /// Gets or sets the Customer ID
        /// </summary>
        public int CustomerID{ get; set; }

        /// <summary>
        /// Gets or sets the Question
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public DateTime CreatedOnUTC { get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public int CreatedBy { get; set; }

    }
}