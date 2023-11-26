//WKK 20190219 MDT-246 Admin Panel > Merchant Referral Reward
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public partial class ConsumptionRewardMerchantReferralListModel : BaseNopModel 
    {
        public ConsumptionRewardMerchantReferralListModel()
        {
            PlatformNameOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.FromDate")]
        [UIHint("Date")] //Jerry 20181102 MSP-436
        public DateTime FromDate { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.ToDate")]
        [UIHint("Date")] //Jerry 20181102 MSP-436
        public DateTime ToDate { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.MerchantRefFrom")] 
        public string MerchantRefFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.MerchantRefTo")] 
        public string MerchantRefTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.PlatformName")]
        public int PlatformID { get; set; }
        public IList<SelectListItem> PlatformNameOptions { get; set; }

        public bool IsRefreshData { get; set; } //Jerry 20181102 MSP-436

    }
}