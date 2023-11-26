using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Custom
{
    public class CustomerMemberTreeDetailsCustom
    {
        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the UserGuid
        /// </summary>
        public string UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the Deposit Wallet Address
        /// </summary>
        public string DepositWalletAddress { get; set; }

        /// <summary>
        /// Gets or sets the Self Score
        /// </summary>
        public string Score_Y { get; set; }

        /// <summary>
        /// Gets or sets the Self Score Percentage
        /// </summary>
        public string ScorePct_Y { get; set; }

        /// <summary>
        /// Gets or sets the Team Score
        /// </summary>
        public string Score_Z { get; set; }

        /// <summary>
        /// Gets or sets the Team Score Percentage
        /// </summary>
        public string ScorePct_Z { get; set; }

        /// <summary>
        /// Gets or sets the Member Quantity
        /// </summary>
        public int MemberQuantity { get; set; }

        /// <summary>
        /// Gets or sets the Contribution
        /// </summary>
        public string Contribution { get; set; }

        #region Types of Rewards
            /// <summary>
            /// Gets or sets the Locked Earning Wallet Balance
            /// </summary>
            public string LockedEarningWalletBalance { get; set; }

            /// <summary>
            /// Gets or sets the Available Balance
            /// </summary>
            public string AvailableBalance { get; set; }

            /// <summary>
            /// Gets or sets the Agency Fee Amount
            /// </summary>
            public string AgencyFeeAmount { get; set; }

            /// <summary>
            /// Gets or sets the Agency Fee Returned
            /// </summary>
            public string AgencyFeeReturned { get; set; }

            /// <summary>
            /// Gets or sets the Agency Fee Reward
            /// </summary>
            public string AgencyFeeReward { get; set; }

            /// <summary>
            /// Gets or sets the Agency Fee Reward Task -> sum of Profit_CP and Profit_CP_Self
            /// </summary>
            public string AgencyFeeRewardTask { get; set; } //Tony Liew 20190102 MSP-635 

            /// <summary>
            /// Gets or sets the Agent Reward
            /// </summary>
            public string AgentReward { get; set; }

            /// <summary>
            /// Gets or sets the Task Reward
            /// </summary>
            public string TaskReward { get; set; }

            /// <summary>
            /// Gets or sets the Merchant Referral Reward
            /// </summary>
            public string MerchantReferralReward { get; set; }
        #endregion
    }
}
