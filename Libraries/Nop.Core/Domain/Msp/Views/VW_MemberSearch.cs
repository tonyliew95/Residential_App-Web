using System;

namespace Nop.Core.Domain.Msp.Views
{
    /// <summary>
    /// Represents a View table VW_MemberSearch
    /// </summary>
    public partial class VW_MemberSearch : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public VW_MemberSearch()
        {            
        }

        public int MemberId { get; set; }
        public int? CustomerId { get; set; }
        public int? WalletId { get; set; }
        public int? ParentID { get; set; }
        public string Username { get; set; }
        public string GlobalGUID { get; set; }
        public string IntroducerGlobalGUID { get; set; }
        public string RecommendIDs { get; set; }
        public int MemberQuantity { get; set; }
        public string UserRole { get; set; }
        public bool IsUSCitizen { get; set; }
        public int Level { get; set; }
        public string WithdrawalWalletAddress { get; set; }
        public int? DepositWalletAddress_ID { get; set; }
        public string DepositWalletAddress { get; set; }
        public decimal? Mbtc { get; set; }
        public decimal? Mbtc_Deposit_Total { get; set; }
        public decimal? Mbtc_Withdrawal_Total { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? Deposit_Total { get; set; }
        public decimal? Consumption { get; set; }
        public decimal? Profit_Total { get; set; }
        public decimal? Score_Y { get; set; }
        public decimal? ScorePct_Y { get; set; }
        public decimal? Score_Z { get; set; }
        public decimal? ScorePct_Z { get; set; }
        public DateTime CreatedOnUtc { get; set; }


    }
}
