using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;

namespace Nop.Core.Domain.Msp.Setting
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class MSP_ScorePct_Setting : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_ScorePct_Setting()
        {
           
        }

        /// <summary>
        /// Gets or sets the BatchID GUID
        /// </summary>
        public int BatchID { get; set; }

        /// <summary>
        /// Gets or sets the Score
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// Gets or sets the Score Percentage
        /// </summary>
        public decimal ScorePct { get; set; }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the created by id 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last updated
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the updated by id 
        /// </summary>
         public int UpdatedBy { get; set; }

    }
}