using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;

namespace Nop.Core.Domain.Msp.Member
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class MSP_MemberTree : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_MemberTree()
        {
            IsSync = false;
        }

        /// <summary>
        /// Gets or sets the customer GUID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the customer GUID
        /// </summary>
        public Guid CustomerGUID { get; set; }

        /// <summary>
        /// Gets or sets the parent ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// Gets or sets the RecommendIDs
        /// </summary>
        public string RecommendIDs { get; set; }

        /// <summary>
        /// Gets or sets the User Global GUID
        /// </summary>
        /// LeeChurn 20181004 MSP-220
        public string GlobalGUID { get; set; }

        /// <summary>
        /// Gets or sets the Introducer Global GUID
        /// </summary>
        /// LeeChurn 20181004 MSP-220
        public string IntroducerGlobalGUID { get; set; }

        /// <summary>
        /// Gets or sets the UserRole
        /// </summary>
        /// LeeChurn 20181004 MSP-220
        public string UserRole { get; set; }
        
        /// <summary>
        /// Gets or sets the user member is US Citizen
        /// </summary>
        public bool IsUSCitizen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating member tree level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the MemberWithdrawalAddress_History_ID
        /// </summary>
        public int? MemberWithdrawalAddress_History_ID { get; set; }

        /// <summary>
        /// Gets or sets the WithdrawalWalletAddress
        /// </summary>
        public string WithdrawalWalletAddress { get; set; }

        /// <summary>
        /// Gets or sets the DepositWalletAddress_ID
        /// </summary>
        public int? DepositWalletAddress_ID { get; set; }

        /// <summary>
        /// Gets or sets the DepositWalletAddress
        /// </summary>
        public string DepositWalletAddress { get; set; }

        /// <summary>
        /// Gets or sets the IPAddress_Finsys
        /// </summary>
        public string IPAddress_Finsys { get; set; }

        /// <summary>
        /// Gets or sets the IPAddress_Game
        /// </summary>
        public string IPAddress_Game { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the created by id 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last updated
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the created by id 
        /// </summary>
         public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the ForgotPwdSecurityAttempt
        /// </summary>
        public Int16? ForgotPwdSecurityAttempt { get; set; }

        /// <summary>
        /// Gets or sets the ForgotPwdSecurityAttempt
        /// </summary>
        public DateTime? ForgotPwdSecurityAttemptDate { get; set; }

        /// <summary>
		/// Gets or sets the IsSync for syncronization between DBLive env and DBAudit env
		/// </summary>
        /// WilliamHo 20181001
		public bool IsSync { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Limit
        /// </summary>
        /// Tony Liew 20181121 MDT-6 
        public decimal WithdrawalLimit { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Limit VIP
        /// </summary>
        /// Tony Liew 20181121 MDT-6 
        public decimal WithdrawalLimitVIP { get; set; }

        /// <summary>
        /// Gets or sets the user is Withdrawal Enabled
        /// </summary>
        /// Tony Liew 20181121 MDT-6 
        public bool IsWithdrawalEnabled { get; set; }

        /// <summary>
        /// Gets or sets the Language Code
        /// </summary>
        /// Tony Liew 20181121 MDT-6
        public string LanguageCode { get; set; }
        
        /// <summary>
        /// Gets or sets the CurrMaxDepositPlanAmt
        /// </summary>
        public decimal CurrMaxDepositPlanAmt { get; set; }

        //wailiang 20190301 MDT-286 \/
        /// <summary>
        /// Gets or sets the Role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the IsRoleUpgradeEligible
        /// </summary>
        public bool IsRoleUpgradeEligible { get; set; }

        /// <summary>
        /// Gets or sets the RoleUpgradeOnUtc
        /// </summary>
        public DateTime? RoleUpgradeOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the RoleMinDepositMBtc
        /// </summary>
        public decimal? RoleMinDepositMBtc { get; set; }

        /// <summary>
        /// Gets or sets the RoleMinConsumptionMbtc
        /// </summary>
        public decimal? RoleMinConsumptionMbtc { get; set; }

        /// <summary>
        /// Gets or sets the RoleMemberDepositMbtc
        /// </summary>
        public decimal? RoleMemberDepositMbtc { get; set; }

        /// <summary>
        /// Gets or sets the RoleMemberConsumptionMbtc
        /// </summary>
        public decimal? RoleMemberConsumptionMbtc { get; set; }

        /// <summary>
        /// Gets or sets the RoleUpgradeVia
        /// </summary>
        public string RoleUpgradeVia { get; set; }

        /// <summary>
        /// Gets or sets the AgentAgreementPopUp
        /// </summary>
        public bool AgentAgreementPopUp { get; set; }

        /// <summary>
        /// Gets or sets the DepositUpgradeCount
        /// </summary>
        public int DepositUpgradeCount { get; set; }
        //wailiang 20190301 MDT-286 /\

        /// <summary>
        /// Gets or sets the IsUpgradeAllow
        /// </summary>
        public bool IsUpgradeAllow { get; set; } //wailiang 20190304 MDT-286
    }
}