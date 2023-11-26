using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Calculation
{
    /// <summary>
    /// Represents a MSP_Deposit
    /// </summary>
    public partial class MSP_Deposit_Offset : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_Offset()
        {

        }

        /// <summary>
        /// Gets or sets the BatchID
        /// </summary>
        public Guid BatchID { get; set; }

        /// <summary>
        /// Gets or sets the Deposit ID (MSP_Deposit)
        /// </summary>
        public int DepositID { get; set; }

        /// <summary>
        /// Gets or sets the Customer Id
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Wallet ID
        /// </summary>
        public int WalletID { get; set; }

        /// <summary>
        /// Gets or sets the Rule No
        /// </summary>
        public string RuleNo { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Amount
        /// </summary>
        public decimal DepositAmt { get; set; }

        /// <summary>
        /// Gets or sets the B Score Y
        /// </summary>
        public decimal BScoreY { get; set; }

        /// <summary>
        /// Gets or sets the A Deposit Balance
        /// </summary>
        public decimal ADepositBal { get; set; }

        /// <summary>
        /// Gets or sets the A Deposit Balance After Offset
        /// </summary>
        public decimal ADepositBalAfterOffset  { get; set; }

        /// <summary>
        /// Gets or sets the Overflow balance 
        /// </summary>
        public decimal OverflowBal { get; set; }

        /// <summary>
        /// Gets or sets a Remark
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// Gets or sets a Remark
        /// </summary>
        public string SysRemark { get; set; }

        /// <summary>
        /// Gets or sets a Status
        /// </summary
        private MSP_Deposit_Offset_Status _StatusEnum;
        public virtual MSP_Deposit_Offset_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Deposit_Offset_Status>(); }
            set { _StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_Deposit_Offset_Status>(value); }
        }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}
