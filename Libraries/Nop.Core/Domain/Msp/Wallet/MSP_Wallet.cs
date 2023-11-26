using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Wallet
{
    public partial class MSP_Wallet : BaseEntity
    {
        public MSP_Wallet()
        {
            IsSync = false;
        }

        /// <summary>
        /// Gets or sets the CustomerID
        /// </summary>
        public int CustomerID { get; set; }
        
        /// <summary>
        /// Gets or sets the Mbtc
        /// </summary>
        public decimal Mbtc { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc Float
        /// </summary>
        public decimal Mbtc_Float { get; set; }

        /// <summary>
        /// Gets or sets the Deposit
        /// </summary>
        public decimal Deposit { get; set; }

        /// <summary>
        /// Gets or sets the Consumption
        /// </summary>
        public decimal Consumption { get; set; }

        /// <summary>
        /// Gets or sets the Score - Y
        /// </summary>
        public decimal Score_Y { get; set; }

        /// <summary>
        /// Gets or sets the Score Percentage - Y
        /// </summary>
        public decimal ScorePct_Y { get; set; }

        /// <summary>
        /// Gets or sets the Score - Membership Score Percentage ID
        /// </summary>
        public int MemberScorePct_Y_ID { get; set; }

        /// <summary>
        /// Gets or sets the Score - Z
        /// </summary>
        public decimal Score_Z { get; set; }

        /// <summary>
        /// Gets or sets the Score Percentage - Z
        /// </summary>
        public decimal ScorePct_Z { get; set; }

        /// <summary>
        /// Gets or sets the Score - Membership Score Percentage ID
        /// </summary>
        public int MemberScorePct_Z_ID { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Profit
        /// </summary>
        public decimal Profit_DP { get; set; }

        /// <summary>
        /// Gets or sets the Consumption Profit
        /// </summary>
        public decimal Profit_CP { get; set; }

        /// <summary>
        /// Gets or sets the Consumption Self Profit
        /// </summary>
        public decimal Profit_CP_Self { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Float Profit 5% 
        /// </summary>
        public decimal Profit_DP_Float { get; set; }

        /// <summary>
        /// Gets or sets the Total Withdrawal Refund
        /// </summary>
        /// Eric 20181001 New column added
        public decimal Mbtc_Withdrawal_Refund_Total { get; set; }

        /// <summary>
        /// Gets or sets the Total Profit (Consumption + Deposit Profit)
        /// </summary>
        public decimal Profit_Total { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc Withdrawal Total
        /// </summary>
        public decimal Mbtc_Withdrawal_Total { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc Deposit Total
        /// </summary>
        public decimal Mbtc_Deposit_Total { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Total
        /// </summary>
        public decimal Deposit_Total { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Return Total
        /// </summary>
        public decimal Deposit_Return_Total { get; set; }

        /// <summary>
        /// Gets or sets the Previous Mbtc
        /// </summary>
        public decimal Mbtc_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Previous Mbtc Float
        /// </summary>
        public decimal Mbtc_Float_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Previous Deposit
        /// </summary>
        public decimal Deposit_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Previous Consumption
        /// </summary>
        public decimal Consumption_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Previous Score - Y
        /// </summary>
        public decimal Score_Y_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Previous Score - Z
        /// </summary>
        public decimal Score_Z_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Previous Deposit Profit
        /// </summary>
        public decimal Profit_DP_Prev { get; set; }

        /// <summary>
        /// Gets or sets the previous Consumption Profit
        /// </summary>
        public decimal Profit_CP_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Previous Consumption Self Profit 
        /// </summary>
        public decimal Profit_CP_Self_Prev { get; set; }

        /// <summary>
        /// Gets or sets the previous Deposit Float Profit 5%
        /// </summary>
        public decimal Profit_DP_Float_Prev { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc Encryption
        /// </summary>
        public string Mbtc_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Mbtc Float Encryption
        /// </summary>
        public string Mbtc_Float_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Encryption
        /// </summary>
        public string Deposit_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Consumption Encryption
        /// </summary>
        public string Consumption_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Score Y Encryption 
        /// </summary>
        public string Score_Y_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Score Z Encryption 
        /// </summary>
        public string Score_Z_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Score Y Encryption 
        /// </summary>
        public string ScorePct_Y_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Score Z Encryption 
        /// </summary>
        public string ScorePct_Z_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Profit Encryption
        /// </summary>
        public string Profit_DP_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Consumption Profit Encryption
        /// </summary>
        public string Profit_CP_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Consumption Self Profit Encryption
        /// </summary>
        public string Profit_CP_Self_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Float Profit 5% Encryption
        /// </summary>
        public string Profit_DP_Float_Enc { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
		/// Gets or sets the IsSync for syncronization between DBLive env and DBAudit env
		/// </summary>
        /// WilliamHo 20181001
		public bool IsSync { get; set; }

		/// <summary>
		/// Gets or sets the Guaranteed Total
		/// </summary>
		public decimal Guaranteed_Total { get; set; }

		/// <summary>
		/// Gets or sets the MerchantReferral Total
		/// </summary>
		public decimal MerchantReferral_Total { get; set; }

	}
}
