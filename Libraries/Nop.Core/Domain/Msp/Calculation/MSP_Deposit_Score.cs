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
    public partial class MSP_Deposit_Score : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_Score()
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
        /// Gets or sets the Sequence No
        /// </summary>
        public int SeqNo { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Type
        /// </summary>
        private MSP_Deposit_Score_TransactionType _TransactionTypeEnum;
        public virtual MSP_Deposit_Score_TransactionType TransactionTypeEnum
        {
            get { return _TransactionTypeEnum; }
            set { _TransactionTypeEnum = value; }
        }
        public string TransactionType
        {
            get { return _TransactionTypeEnum.ToValue<MSP_Deposit_Score_TransactionType>(); }
            set { _TransactionTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_Deposit_Score_TransactionType>(value); }
        }

        private MSP_Deposit_Score_AmountType _AmountTypeEnum;
        public virtual MSP_Deposit_Score_AmountType AmountTypeEnum
        {
            get { return _AmountTypeEnum; }
            set { _AmountTypeEnum = value; }
        }
        public string AmountType
        {
            get { return _AmountTypeEnum.ToValue<MSP_Deposit_Score_AmountType>(); }
            set { _AmountTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_Deposit_Score_AmountType>(value); }
        }

        /// <summary>
        /// Gets or sets the Score Amount
        /// </summary>
        public decimal ScoreAmt { get; set; }

        /// <summary>
        /// Gets or sets the Accumucated Total Score
        /// </summary>
        public decimal ScoreAmtBal { get; set; }

        /// <summary>
        /// Gets or sets the Accumucated Total Score
        /// </summary>
        public decimal ScoreAmtBalAfterScore { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Amount
        /// </summary>
        public decimal DepositAmt { get; set; }

        /// <summary>
        /// Gets or sets the Setting ID
        /// </summary>
        public int Setting_ID { get; set; }

        /// <summary>
        /// Gets or sets the Setting Score Percentage
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

        /// <summary>
        /// Gets or sets a Status
        /// </summary
        private MSP_Deposit_Score_Status _StatusEnum;
        public virtual MSP_Deposit_Score_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Deposit_Score_Status>(); }
            set { _StatusEnum = (MSP_Deposit_Score_Status)Enum.Parse(typeof(MSP_Deposit_Score_Status), value); }
        }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}
