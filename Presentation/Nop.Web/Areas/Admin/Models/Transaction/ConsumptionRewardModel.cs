using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Msp.Transaction;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class ConsumptionRewardModel : BaseNopModel //wailiang 20180906 MSP-98
    {
        public ConsumptionRewardModel()
        {
        }
        //Get CustomerId from ConsumptionRewardSelf
        public int CustomerId { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.TeamConsumptionReward.Fields.Date")]
        public string Date { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.TeamConsumptionReward.Fields.PlatformName")]
        public string PlatformName { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardHistory.Fields.TotalDepositReturned.mBTC")]
        public string TotalDepositReturned { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardHistory.Fields.TotalMembershipReward.mBTC")]
        public string TotalMembershipReward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardHistory.Fields.TotalConsumptionReward.mBTC")]
        public string TotalConsumptionReward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardHistory.Fields.TotalHonoraryCitizenReward.mBTC")]
        public string TotalHonoraryCitizenReward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.ConsumptionRewardHistory.Fields.GrandTotalReward.mBTC")]
        public string GrandTotalReward { get; set; }
    }
}
