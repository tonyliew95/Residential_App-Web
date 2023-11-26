using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Enumeration;

namespace Nop.Core.Domain.Msp.Setting
{
    /// <summary>
    /// MSP_Deposit_Plan table
    /// </summary>
    public partial class MSP_Deposit_Plan : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_Plan()
        {
        }

        /// <summary>
        /// Gets or sets the BatchID
        /// </summary>
        public int BatchID { get; set; }

        /// <summary>
        /// Gets or sets the DepositAmt
        /// </summary>
        public decimal DepositAmt { get; set; }

        /// <summary>
        /// Gets or sets PurchaseLocked
        /// </summary>
        public bool PurchaseLocked { get; set; }

        /// <summary>
        /// Gets or sets HasLimit
        /// </summary>
        public bool HasLimit { get; set; }

        /// <summary>
        /// Gets or sets UnitLimit
        /// </summary>
        public int UsedLimit { get; set; }

        /// <summary>
        /// Gets or sets UsedCount
        /// </summary>
        public int UsedCount { get; set; }

        /// <summary>
        /// Gets or sets CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        //wailiang 20190301 MDT-255 \/
        /// <summary>
        /// Gets or sets IsPublished
        /// </summary>
        public bool IsPublished { get; set; }
        //wailiang 20190301 MDT-255 /\
    }
}