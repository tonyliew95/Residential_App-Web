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
    public partial class MSP_Deposit_Rule : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_Rule()
        {

        }

        /// <summary>
        /// Gets or sets the BatchID
        /// </summary>
        public Guid BatchID { get; set; }

        /// <summary>
        /// Gets or sets the Customer Id
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
        /// Gets or sets the Downline ID
        /// </summary>
        public int DownlineID { get; set; }

        /// <summary>
        /// Gets or sets the Rule No
        /// </summary>
        public string RulkeNo { get; set; }

        /// <summary>
        /// Gets or sets the Downline Wallet ID
        /// </summary>
        public int DownlineWalletID { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Amount
        /// </summary>
        public decimal DepositAmt { get; set; }

        /// <summary>
        /// Gets or sets the A Deposit Balance
        /// </summary>
        public decimal ADepositBal { get; set; }

        /// <summary>
        /// Gets or sets the A Score Y
        /// </summary>
        public decimal AScoreY { get; set; }

        /// <summary>
        /// Gets or sets the A Score Z
        /// </summary>
        public decimal AScoreZ { get; set; }

        /// <summary>
        /// Gets or sets the A Score Y Percentage
        /// </summary>
        public decimal AScoreYPct { get; set; }

        /// <summary>
        /// Gets or sets the A Score Z Percentage
        /// </summary>
        public decimal AScoreZPct { get; set; }

        /// <summary>
        /// Gets or sets the B Score Y
        /// </summary>
        public decimal BScoreY  { get; set; }

        /// <summary>
        /// Gets or sets the B Score Z
        /// </summary>
        public decimal BScoreZ { get; set; }

        /// <summary>
        /// Gets or sets the B Score Y Percentage
        /// </summary>
        public decimal BScoreYPct { get; set; }

        /// <summary>
        /// Gets or sets the B Score Z Percentage
        /// </summary>
        public decimal BScoreZPct { get; set; }

        /// <summary>
        /// Gets or sets the formula
        /// </summary>
        public string Formula { get; set; }

        /// <summary>
        /// Gets or sets System Remark
        /// </summary>
        public string SysRemark { get; set; }


        private MSP_Deposit_Rule_Status _StatusEnum;
        public virtual MSP_Deposit_Rule_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Deposit_Rule_Status>(); }
            set { _StatusEnum = (MSP_Deposit_Rule_Status)Enum.Parse(typeof(MSP_Deposit_Rule_Status), value); }
        }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}
