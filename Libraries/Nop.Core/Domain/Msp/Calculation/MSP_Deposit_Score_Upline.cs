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
    public partial class MSP_Deposit_Score_Upline : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_Score_Upline()
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
        /// Gets or sets the MemberLevel
        /// </summary>
        public int MemberLevel { get; set; }

        /// <summary>
        /// Gets or sets the Rule No
        /// </summary>
        public string RuleNo { get; set; }

        /// <summary>
        /// Gets or sets the Sequence NO
        /// </summary>
        public int SeqNo{ get; set; }


        /// <summary>
        /// Gets or sets a Status
        /// </summary
        private MSP_Deposit_Score_Upline_TransactionType _TransactionTypeEnum;
        public virtual MSP_Deposit_Score_Upline_TransactionType TransactionTypeEnum
        {
            get { return _TransactionTypeEnum; }
            set { _TransactionTypeEnum = value; }
        }
        public string TransactionType
        {
            get { return _TransactionTypeEnum.ToValue<MSP_Deposit_Score_Upline_TransactionType>(); }
            set { _TransactionTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_Deposit_Score_Upline_TransactionType>(value); }
        }

        /// <summary>
        /// Gets or sets a Amount Type
        /// </summary
        private MSP_Deposit_Score_Upline_AmountType _AmountTypeEnum;
        public virtual MSP_Deposit_Score_Upline_AmountType AmountTypeEnum
        {
            get { return _AmountTypeEnum; }
            set { _AmountTypeEnum = value; }
        }
        public string AmountType
        {
            get { return _AmountTypeEnum.ToValue<MSP_Deposit_Score_Upline_AmountType>(); }
            set { _AmountTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_Deposit_Score_Upline_AmountType>(value); }
        }

        /// <summary>
        /// Gets or sets the DepositAmt
        /// </summary>
        public decimal DepositAmt { get; set; }

        /// <summary>
        /// Gets or sets the OverflowBal
        /// </summary>
        public decimal OverflowBal { get; set; }

        /// <summary>
        /// Gets or sets the Score Amount
        /// </summary>
        public decimal ScoreAmt { get; set; }

        /// <summary>
        /// Gets or sets the Score Amount Balance
        /// </summary>
        public decimal ScoreAmtBal { get; set; }

        /// <summary>
        /// Gets or sets the Score Amount Balance After Score
        /// </summary>
        public decimal ScoreAmtBalAfterScore { get; set; }

        /// <summary>
        /// Gets or sets the Setting ID
        /// </summary>
        public int Setting_ID { get; set; }

        /// <summary>
        /// Gets or sets the Setting Percentage
        /// </summary>
        public decimal Setting_ScorePct { get; set; }

        /// <summary>
        /// Gets or sets a Remark
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
