using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.MemberTree
{
    public class MemberTreeModel
    {
        #region filter
        [NopResourceDisplayName("Admin.MemberTree.Filter.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.MemberTree.Filter.GlobalGuid")]
        public string GlobalGuid { get; set; }

        [NopResourceDisplayName("Admin.MemberTree.Filter.IntroducerGlobalGuid")]
        public string IntroducerGlobalGuid { get; set; }

        [NopResourceDisplayName("Admin.MemberTree.Filter.DateFrom")]
        public DateTime? DateFrom { get; set; }

        [NopResourceDisplayName("Admin.MemberTree.Filter.DateTo")]
        public DateTime? DateTo { get; set; }

        public int CustomerId { get; set; }
        #endregion

        public class MemberTree
        {
            /// <summary>
            /// Gets or sets the CustomerId
            /// </summary>
            public int CustomerId { get; set; }

            /// <summary>
            /// Gets or sets the Username
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// Gets or sets the GlobalGuid
            /// </summary>
            public string GlobalGuid { get; set; }

            /// <summary>
            /// Gets or sets the IntroducerGlobalGuid
            /// </summary>
            public string IntroducerGlobalGuid { get; set; }

            /// <summary>
            /// Gets or sets the CreatedOnUtc
            /// </summary>
            public DateTime? CreatedOnUtc { get; set; }
        }

        //response
        public IList<MemberTree> MemberTreeList { get; set; }


        public class CustomerMemberTree
        {
            /// <summary>
            /// Gets or sets the Username
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// Gets or sets the CustomerId
            /// </summary>
            public int CustomerId { get; set; }

            /// <summary>
            /// Gets or sets the ParentId
            /// </summary>
            public int? ParentId { get; set; }
        }

        public IList<CustomerMemberTree> CustomerMemberTreeList { get; set; }

        public class DownlineDTO
        {
            public int id { get; set; }
            public string text { get; set; }
            public bool hasChildren { get; set; }
            public virtual List<DownlineDTO> children { get; set; }
        }

        public IList<DownlineDTO> DownlineMemberTreeList { get; set; }


        public class CustomerDetails
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

        public IList<CustomerDetails> CustomerMemberTreeDetails { get; set; }

    }
}
