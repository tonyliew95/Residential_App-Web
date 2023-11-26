using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Transaction
{
    /// <summary>
    /// Represents a MSP_Deposit
    /// </summary>
    public partial class MSP_Settlement : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Settlement()
        {
            IsSync = false;
        }

        /// <summary>
        /// Gets or sets the Customer Id
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Wallet Id
        /// </summary>
        public int WalletID { get; set; }

        /// <summary>
        /// Gets or sets the Settle Amount
        /// </summary>
        public decimal SettleAmt { get; set; }
        
        /// <summary>
        /// Gets or sets the Mbtc Float
        /// </summary>
        public decimal MbtcBal { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc After Settle
        /// </summary>
        public decimal MbtcAfterSettle { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc Float Balance
        /// </summary>
        public decimal MbtcFloatBal { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc Float After Settle
        /// </summary>
        public decimal MbtcFloatAfterSettle { get; set; }

        /// <summary>
        /// Gets or sets the Completed By Batch ID
        /// </summary>
        public Guid? CompletedByBatchID { get; set; }

        /// <summary>
        /// Gets or sets a System Remark
        /// </summary>
        public string SysRemark { get; set; }

        /// <summary>
        /// Gets or sets a Status
        /// </summary
        private MSP_Settlement_Status _StatusEnum;
        public virtual MSP_Settlement_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Settlement_Status>(); }
            set { _StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_Settlement_Status>(value); }
        }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of updated 
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the sync audit db
        /// </summary>
        public bool IsSync { get; set; }

    }
}
