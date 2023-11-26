using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class ConsumptionRewardSelfModel : BaseNopModel
    {
        public ConsumptionRewardSelfModel()
        {
        }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.Date")]
        public DateTime Date { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.PlatformName")]
        public string PlatformName { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.DepositReturned")]
        public string Deposit_Returned { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.MembershipReward")]
        public string Membership_Reward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.ConsumptionReward")]
        public string Consumption_Reward { get; set; }

        //[NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.MerchantRefReward")] //Atiqah 20190219 MDT-244
        //public string Merchant_Ref_Reward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.TotalReward")]
        public string Total_Reward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardHistory.Fields.TotalHonoraryCitizenReward.mBTC")] // WKK 20190222 MDT-248
        public string TotalHonoraryCitizenReward { get; set; }
    }
}
