using Nop.Web.Framework.Mvc.ModelBinding;
using System;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class DepositRewardHistoryModel //Atiqah 20180924 MSP-150
    {
        public DepositRewardHistoryModel()
        {
        }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.Date")]
        public DateTime Date { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.Username")]
        public string Username { get; set; }

        //[NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.ContributedBy")]
        //public int ContributedBy { get; set; } //Jerry 20181009 MSP-209
        //public string ContributedBy { get; set; } //Jerry 20181009 MSP-209  //Atiqah 20190221 MDT-257

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.DepositAmount")]
        public string Deposit_Amt { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.DepositReturned")]
        public string Deposit_Returned { get; set; }

        /*[NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.DepositReward")]  //Atiqah 20190221 MDT-257
        public string Deposit_Reward { get; set; }*/

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.DepositReturnedReward")]
        public string Deposit_Returned_Reward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.DepositOverflowReward")]
        public string Deposit_Overflow_Reward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.ExtraReward")]
        public string Extra_Reward { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.TotalReward")]
        public string Total_Reward { get; set; }

        //Atiqah 20190221 MDT-257 \/
        /*[NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.SelfScorePct")]
        public string SelfScore_Pct { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.SelfScore")]
        public string SelfScore { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.TeamScorePct")]
        public string TeamScore_Pct { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.DepositRewardHistory.Fields.TeamScore")]
        public string TeamScore { get; set; }*/
        //Atiqah 20190221 MDT-257 /\
    }
}
