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
    public partial class MSP_Deposit_Profit : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_Profit()
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
        /// Gets or sets the Customer Type
        /// </summary>
        public string CustomerType { get; set; }

        /// <summary>
        /// Gets or sets the MemnberLevel
        /// </summary>
        public int MemnberLevel { get; set; }

        /// <summary>
        /// Gets or sets the Downline ID
        /// </summary>
        public int DownlineID { get; set; }

        /// <summary>
        /// Gets or sets the Downline Wallet ID
        /// </summary>
        public int DownlineWalletID { get; set; }

        /// <summary>
        /// Gets or sets the ProfitType
        /// </summary>
        public string ProfitType { get; set; }

        /// <summary>
        /// Gets or sets the Rule No
        /// </summary>
        public string RuleNo { get; set; }

        /// <summary>
        /// Gets or sets the Sequence NO
        /// </summary>
        public int SeqNo{ get; set; }

        /// Gets or sets the Deposit Amount
        /// </summary>
        public decimal DepositAmt { get; set; }

        /// Gets or sets the Offset Amount
        /// </summary>
        public decimal OffsetAmt { get; set; }

        /// <summary>
        /// Gets or sets the OverflowBal
        /// </summary>
        public decimal OverflowBal { get; set; }

        /// <summary>
        /// Gets or sets the A Score Y Percentage
        /// </summary>
        public decimal AScoreYPct { get; set; }

        /// <summary>
        /// Gets or sets the A Score Y Percentage
        /// </summary>
        public decimal AScoreZPct { get; set; }

        /// <summary>
        /// Gets or sets the B Score Y Percentage
        /// </summary>
        public decimal BScoreYPct { get; set; }

        /// <summary>
        /// Gets or sets the B Score Z Percentage
        /// </summary>
        public decimal BScoreZPct { get; set; }

        /// <summary>
        /// Gets or sets the Profit Amount Entitle
        /// </summary>
        public decimal ProfitAmt_Entitle { get; set; }

        /// <summary>
        /// Gets or sets the Profit Amount
        /// </summary>
        public decimal ProfitAmt { get; set; }

        /// <summary>
        /// Gets or sets the Unclaim Previous
        /// </summary>
        public decimal Unclaim_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Unclaim Remain
        /// </summary>
        public decimal Unclaim_Remain { get; set; }

        /// <summary>
        /// Gets or sets the Unclaimed
        /// </summary>
        public decimal Unclaimed { get; set; }

        /// <summary>
        /// Gets or sets a Formula
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// Gets or sets a Remark
        /// </summary>
        public string SysRemark { get; set; }

        private MSP_Deposit_Score_Upline_Status _StatusEnum;
        public virtual MSP_Deposit_Score_Upline_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Deposit_Score_Upline_Status>(); }
            set { _StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_Deposit_Score_Upline_Status>(value); }

        }
        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}
