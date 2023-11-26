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
    public partial class MSP_Deposit : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit()
        {
            IsSync = false;
        }

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
        /// Gets or sets the Deposit Plan ID 
        /// </summary>
        public int DepositPlanID { get; set; }   //LeeChurn 20180914 MSP-129

        /// <summary>
        /// Gets or sets the Mbtc Amount
        /// </summary>
        public decimal DepositAmt { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Amount Encryption
        /// </summary>
        public string DepositAmt_Enc { get; set; }

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
        private MSP_Deposit_Status _StatusEnum; 
        public virtual MSP_Deposit_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; } 
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Deposit_Status>(); }
            set{_StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_Deposit_Status>(value);}
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
        /// Gets or sets sync audit db flag 
        /// </summary>
        public bool IsSync { get; set; }

    }
}
