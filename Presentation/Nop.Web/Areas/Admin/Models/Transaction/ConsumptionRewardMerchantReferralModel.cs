//WKK 20190219 MDT-246 Admin Panel > Merchant Referral Reward
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class ConsumptionRewardMerchantReferralModel : BaseNopModel
    {
        public ConsumptionRewardMerchantReferralModel()
        {
        }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.Date")]
        public DateTime Date { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.PlatformName")]
        public string PlatformName { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.Fields.MerchantRefReward")] 
        public string Merchant_Ref_Reward { get; set; }
    }
}
