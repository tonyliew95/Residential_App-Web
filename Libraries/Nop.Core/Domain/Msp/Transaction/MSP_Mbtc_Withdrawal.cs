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
    public partial class MSP_Mbtc_Withdrawal : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Mbtc_Withdrawal()
        {
            IsSync = false;
        }

        /// <summary>
        /// Gets or sets the Batch Id
        /// </summary>
        public Guid BatchID { get; set; }

        /// <summary>
        /// Gets or sets the Wallet Address
        /// </summary>
        public string WalletAddress { get; set; }

        /// <summary>
        /// Gets or sets the Cutsomer ID
        /// </summary>
        public int CustomerID   {get; set;}

        /// <summary>
        /// Gets or sets the Wallet ID
        /// </summary>
        public int WalletID { get; set; }

        /// <summary>
        /// Gets or sets the Withdraw Amount
        /// </summary>
        public decimal WithdrawAmt { get; set; }

        // MSP-36	Crypto Service Backend API: Upload/Update Withdrawal Request Status to Processing 2018-08-30 WKK
        // MSP-159	Hash withdrawal amount for crypto purpose 2018-09-21 Erictan 
        /// <summary>
        /// Gets or sets the Net Withdraw Amount truncated to 5 decimal points
        /// </summary>
        public decimal NetWithdrawAmt_5Decimal { get; set; }

        // MSP-159	Hash withdrawal amount for crypto purpose 2018-09-21 Erictan 
        /// <summary>
        /// Gets or sets the Net Withdraw Amount 5 Decimals Hashvalue
        /// </summary>
        public string NetWithdrawAmt_5DecimalHash { get; set; }

        /// <summary>
        /// Gets or sets the Truncate Profit
        /// </summary>
        public decimal TruncateProfit { get; set; } //WilliamHo 20181024

        //WilliamHo 20180911 Not require in front end \/
        /* Refer data dictionary V1.34
		/// <summary>
		/// Gets or sets the Withdraw Amount Encryption
		/// </summary>
		public string WithdrawAmt_Enc { get; set; }
        */
        //WilliamHo 20180911 /\

        /// <summary>
        /// Gets or sets a Remark
        /// </summary>
        public string Remark { get; set; }

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
        /// Gets or sets the date and time of created 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public int UpdatedBy { get; set; }


		// MSP-36	Crypto Service Backend API: Upload/Update Withdrawal Request Status to Processing 2018-08-20 WKK
		/// <summary>
		/// Gets or sets the Block Chain Transaction ID
		/// </summary>
		public string BlockChainTxId { get; set; }

        // MSP-159	Hash withdrawal amount for crypto purpose 2018-09-21 Erictan 
        /// <summary>
        /// Gets or sets the Block Chain Transaction Fees
        /// </summary>
        public decimal BlockChainTxFees { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Fee ID
        /// </summary>
        public int TransactionFeesID { get; set; } //WilliamHo 20180914 MSP-135

        /// <summary>
        /// Gets or sets the Transaction Fee
        /// </summary>
        public decimal TxFee { get; set; } //wailiang 20180912 MSP-95

        /// <summary>
        /// Gets or sets the Net withdrawal Amount mBTC
        /// </summary>
        public decimal NetWithdrawAmt { get; set; } //wailiang 20180912 MSP-95

        /// <summary>
        /// Gets or sets the [ErrorCode]
        /// </summary>
        public int? ErrorCode { get; set; }

		/// <summary>
		/// Gets or sets the ErrorMessage
		/// </summary>
		public string ErrorMessage { get; set; }

        /// <summary>
		/// Gets or sets sync audit db
		/// </summary>
		public bool IsSync { get; set; }

        //wailiang 20190116 MDT-195 \/
        /// <summary>
		/// Gets or sets Refund Status
		/// </summary>
		public string RefundProcessStatus { get; set; }
        //wailiang 20190116 MDT-195 /\
    }
}
