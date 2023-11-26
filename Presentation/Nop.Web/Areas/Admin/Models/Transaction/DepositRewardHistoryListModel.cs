using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class DepositRewardHistoryListModel //Atiqah 20180924 MSP-150
    {
        public DepositRewardHistoryListModel()
        {
            this.DepositRewardHistoryList = new List<DepositRewardHistoryModel>();
            this.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            this.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
        }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DateFrom")]
        [UIHint("DateFrom")]
        public DateTime DateFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DateTo")]
        [UIHint("DateTo")]
        public DateTime DateTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.Username")]
        public string Username { get; set; }

        /*[NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.ContributedBy")]  //Atiqah 20190221 MDT-257
        public string ContributedBy { get; set; }*/ //Jerry 20181009 MSP-209

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositAmountFrom")]
        public string DepositAmountFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositAmountTo")]
        public string DepositAmountTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositReturnedFrom")]
        public string DepositReturnedFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositReturnedTo")]
        public string DepositReturnedTo { get; set; }

        //Atiqah 20190221 MDT-257 \/
        /*[NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositRewardFrom")]
        public string DepositRewardFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositRewardTo")]
        public string DepositRewardTo { get; set; }*/
        //Atiqah 20190221 MDT-257 /\

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositReturnedRewardFrom")]
        public string DepositReturnedRewardFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositReturnedRewardTo")]
        public string DepositReturnedRewardTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositOverflowRewardFrom")]
        public string DepositOverflowRewardFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.DepositOverflowRewardTo")]
        public string DepositOverflowRewardTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.ExtraRewardFrom")]
        public string ExtraRewardFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.ExtraRewardTo")]
        public string ExtraRewardTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.TotalRewardFrom")]
        public string TotalRewardFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.TotalRewardTo")]
        public string TotalRewardTo { get; set; }

        //Atiqah 20190221 MDT-257 \/
        /*[NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.SelfScorePctFrom")]
        public string SelfScorePctFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.SelfScorePctTo")]
        public string SelfScorePctTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.SelfScoreFrom")]
        public string SelfScoreFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.SelfScoreTo")]
        public string SelfScoreTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.TeamScorePctFrom")]
        public string TeamScorePctFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.TeamScorePctTo")]
        public string TeamScorePctTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.TeamScoreFrom")]
        public string TeamScoreFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.List.TeamScoreTo")]
        public string TeamScoreTo { get; set; }*/
        //Atiqah 20190221 MDT-257 /\

        public IList<DepositRewardHistoryModel> DepositRewardHistoryList { get; set; }

    }
}
