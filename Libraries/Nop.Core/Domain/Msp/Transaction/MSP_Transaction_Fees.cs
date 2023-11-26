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
    public partial class MSP_Transaction_Fees : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Transaction_Fees()
        {

        }

        /// <summary>
        /// Gets or sets the Transaction Type
        /// </summary>
        //public string TransactionType { get; set; } //WilliamHo 20180914 MSP-135

        ////WilliamHo 20180914 MSP-135 \/
        /// <summary>
        /// Gets or sets a Transaction Type
        /// </summary
        private MSP_Transaction_Fees_TransactionType _TransactionTypeEnum;
        public virtual MSP_Transaction_Fees_TransactionType TransactionTypeEnum
        {
            get { return _TransactionTypeEnum; }
            set { _TransactionTypeEnum = value; }
        }
        public string TransactionType
        {
            get { return _TransactionTypeEnum.ToValue<MSP_Transaction_Fees_TransactionType>(); }
            set { _TransactionTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_Transaction_Fees_TransactionType>(value); }
        }
        //WilliamHo 20180914 MSP-135 /\

        /// <summary>
        /// Gets or sets the Fees Type
        /// </summary>
        //public string FeesType{ get; set; } //WilliamHo 20180914 MSP-135

        ////WilliamHo 20180914 MSP-135 \/
        /// <summary>
        /// Gets or sets a Fees Type
        /// </summary
        private MSP_Transaction_Fees_FeesType _FeesTypeEnum;
        public virtual MSP_Transaction_Fees_FeesType FeesTypeEnum
        {
            get { return _FeesTypeEnum; }
            set { _FeesTypeEnum = value; }
        }
        public string FeesType
        {
            get { return _FeesTypeEnum.ToValue<MSP_Transaction_Fees_FeesType>(); }
            set { _FeesTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_Transaction_Fees_FeesType>(value); }
        }
        //WilliamHo 20180914 MSP-135 /\

        /// <summary>
        /// Gets or sets the Percent Fees
        /// </summary>
        public decimal PercentFees { get; set; }

        /// <summary>
        /// Gets or sets the Fixed Fees
        /// </summary>
        public decimal FixedFees { get; set; }

        /// <summary>
        /// Gets or sets the To BTC Rate
        /// </summary>
        public decimal ToBTCRate { get; set; }

        /// <summary>
        /// Gets or sets a Status
        /// </summary
        private MSP_Transaction_Fees_Status _StatusEnum; 
        public virtual MSP_Transaction_Fees_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; } 
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Transaction_Fees_Status>(); }
            set{_StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_Transaction_Fees_Status>(value);}
        }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets created by Id 
        /// </summary>
        public int CreatedBy { get; set; }

    }
}
