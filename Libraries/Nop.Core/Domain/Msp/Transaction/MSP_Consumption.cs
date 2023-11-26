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
    public partial class MSP_Consumption : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Consumption()
        {
            IsSync = false;
        }

        /// <summary>
        /// Gets or sets the Interface Id from MSP_Interface_Transactions
        /// </summary>
        public int InterfaceID { get; set; }

        /// <summary>
        /// Gets or sets the Customer Id
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Wallet ID
        /// </summary>
        public int WalletID { get; set; }

        /// <summary>
        /// Gets or sets the Parent ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// Gets or sets the Recommend IDs 
        /// </summary>
        public string RecommendIDs { get; set; }

        /// <summary>
        /// Gets or sets the Consumption Amount Distribution Percentage 
        /// </summary>
        public decimal ConsumptionAmtDistPct { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc Amount
        /// </summary>
        public decimal ConsumptionAmt { get; set; }

        /// <summary>
        /// Gets or sets the Consumption Amount Encryption
        /// </summary>
        public string ConsumptionAmt_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Guaranteed Amount Distribution Percentage 
        /// </summary>
        public decimal GuaranteedAmtDistPct { get; set; }

        /// <summary>
        /// Gets or sets the Guaranteed Amount
        /// </summary>
        public decimal GuaranteedAmt { get; set; }

        /// <summary>
        /// Gets or sets the Merchant Referral Amount
        /// </summary>
        public decimal MerchantReferralAmt { get; set; }

        /// <summary>
        /// Gets or sets the Truncate Profit for any overflow amount
        /// </summary>
        public decimal TruncateProfit { get; set; }

        /// <summary>
        /// Gets or sets the Platform Id
        /// </summary>
        public int? PlatformID { get; set; }

        /// <summary>
        /// Gets or sets a Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets a System Remark
        /// </summary>
        public string SysRemark { get; set; }

        /// <summary>
        /// Gets or sets a Completed By BatchID 
        /// </summary>
        public Guid? CompletedByBatchID { get; set; }

        /// <summary>
        /// Gets or sets a Status
        /// </summary
        private MSP_Consumption_Status _StatusEnum;
        public virtual MSP_Consumption_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Consumption_Status>(); }
            set { _StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_Consumption_Status>(value); }
        }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets created by Id 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of updated 
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets updated by Id 
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets IsSync for syncronization between DBLive env and DBAudit env
        /// </summary>
        public bool IsSync { get; set; }

        //WilliamHo 20181217 MSP-598 \/
        /// <summary>
        /// Gets or sets the Merchant Customer Id
        /// </summary>
        public int MerchantCustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Merchant Referral Customer Id
        /// </summary>
        public int MerchantRefCustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Merchant Referral Wallet Id
        /// </summary>
        public int MerchantRefWalletID { get; set; }
        //WilliamHo 20181217 MSP-598 /\
    }
}
