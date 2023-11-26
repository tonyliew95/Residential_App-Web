using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public partial class ConsumptionRewardSelfListModel : BaseNopModel //JK 20180924 MSP-97
    {
        public ConsumptionRewardSelfListModel()
        {
            PlatformNameOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.Username")]
        public string Username { get; set; }

        //[NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.PlatformName")]
        //public List<SelectListItem> PlatformName { get; set; }

        //[NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.PlatformName")]

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.FromDate")]
        //[UIHint("DateFrom")] //Jerry 20181102 MSP-436
        [UIHint("Date")] //Jerry 20181102 MSP-436
        public DateTime FromDate { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.ToDate")]
        //[UIHint("DateTo")] //Jerry 20181102 MSP-436
        [UIHint("Date")] //Jerry 20181102 MSP-436
        public DateTime ToDate { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.DepositReturnFrom")]
        public string DepositReturnFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.DepositReturnTo")]
        public string DepositReturnTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.MemberRewardFrom")]
        public string MemberRewardFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.MemberRewardTo")]
        public string MemberRewardTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.ConsumptionFrom")]
        public string ConsumptionFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.ConsumptionTo")]
        public string ConsumptionTo { get; set; }

        //[NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.MerchantRefFrom")] //Atiqah 20190219 MDT-244
        //public string MerchantRefFrom { get; set; }
        //
        //[NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.MerchantRefTo")] //Atiqah 20190219 MDT-244
        //public string MerchantRefTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.HonoraryCitizenRewardFrom")] // WKK 20190225 MDT-248
        public string HonoraryCitizenRewardFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.HonoraryCitizenRewardTo")] // WKK 20190225 MDT-248
        public string HonoraryCitizenRewardTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.TotalRewardFrom")]
        public string TotalRewardFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.TotalRewardTo")]
        public string TotalRewardTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardSelf.List.PlatformName")]
        public int PlatformID { get; set; }
        public IList<SelectListItem> PlatformNameOptions { get; set; }

        public bool IsRefreshData { get; set; } //Jerry 20181102 MSP-436

    }
}