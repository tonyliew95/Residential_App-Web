using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Msp;
using Nop.Core.Domain.Msp.Custom;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;

namespace Nop.Services.Transaction
{
    public interface ITransactionService
    {
        IPagedList<ConsumptionRewardHistoryCustom> ConsumptionRewardHistoryList(int customerId, DateTime date, int pageIndex = 0, int pageSize = int.MaxValue); //wailiang 20180921 MSP-98

        IPagedList<DepositTransactionCustom> GetDepositDetails(string username = null, DateTime? DateFrom = null, DateTime? DateTo = null, int? TxnNo = null, decimal? DepositAmt = null, string status = null, int pageIndex = 0, int pageSize = int.MaxValue); //Atiqah 20190903 MSP-94 //wailiang 20190115 MDT-194 

        IPagedList<RedeemTransactionsCustom> GetAllRedeemTransactions(string username = null, string walletid = null, string availableBalanceFrom = null, string availableBalanceTo = null, string redeemWalletBalanceFrom = null, string redeemWalletBalanceTo = null, int pageIndex = 0, int pageSize = 1); //JK 20180912 MSP-96

        IPagedList<WithdrawalTransactionCustom> GetAllWithdrawal(string username = null, int? _txnNo = null, decimal? _amt = null, decimal? _txnFees = null, decimal? _netAmt = null, string _withdrawalAddress = null,
            string _blockchainTxId = null, string _status = null, DateTime? DateFrom = null, DateTime? DateTo = null, int pageIndex = 0, int pageSize = int.MaxValue); //wailiang 20180910 MSP-95 //wailiang 20190116 MDT-195

        IPagedList<MSP_Mbtc_Deposit> GetTopUpDetails(string Username, DateTime? DateFrom, DateTime? DateTo, int? TxId, decimal? TopUpAmt, string TopUpAmtMBTCAdd, string BlockchainTxId, string Status
        , int pageIndex = 0, int pageSize = int.MaxValue);//MSP-46 Backend Function: Transaction Listing > Top Up //wailiang 20190116 MDT-193

        IList<SelectListItem> GetPlatformNameValue(); //JK 20180926 MSP-97

        //IPagedList<ConsumptionRewardSelfCustom> GetConsumptionRewardSelfList(DateTime? FromDate, DateTime? ToDate, out int TotalRecord, out int TotalPage
        //    , string username = null
        //    , int platformid = 0
        //    , string DepositReturnFrom = null, string DepositReturnTo = null
        //    , string MemberRewardFrom = null, string MemberRewardTo = null
        //    , string ConsumptionFrom = null, string ConsumptionTo = null
        //    , string MerchantRefFrom = null, string MerchantRefTo = null
        //    , string TotalRewardFrom = null, string TotalRewardTo = null,
        //    int pageIndex = 0, int pageSize = 1
        //    ); //JK 20180926 MSP-97 //wailiang 20181003 MSP-97
        List<ConsumptionRewardSelfCustom> GetConsumptionRewardSelfList(DateTime? FromDate, DateTime? ToDate, string username, int platformid,
           string DepositReturnFrom, string DepositReturnTo, string MemberRewardFrom, string MemberRewardTo, string ConsumptionFrom,
           string ConsumptionTo, /*string MerchantRefFrom, string MerchantRefTo,*/ string TotalRewardFrom, string TotalRewardTo); //Atiqah 20190219 MDT-244

        // 20190222 WKK MDT-248 Admin Panel > Team Task Reward
        List<ConsumptionRewardSelfCustom> GetConsumptionRewardTeamList(DateTime? FromDate, DateTime? ToDate, string username, int platformid,
            string DepositReturnFrom, string DepositReturnTo, string MemberRewardFrom, string MemberRewardTo, string ConsumptionFrom,
            string ConsumptionTo, string HonoraryCitizenRewardFrom, string HonoraryCitizenRewardTo, string TotalRewardFrom, string TotalRewardTo); 

        //WKK 20190219 MDT-246 Admin Panel > Merchant Referral Reward
        List<ConsumptionRewardSelfCustom> GetConsumptionRewardMerchantReferralList(DateTime? FromDate, DateTime? ToDate, string username, int platformid,
           string MerchantRefFrom, string MerchantRefTo); 

        IList<DepositRewardHistoryCustom> GetDepositRewardList(DateTime? DateFrom, DateTime? DateTo
            , string Username
            //, string ContributedBy //Jerry 20181009 MSP-209 //Atiqah 20190221 MDT-257
            , string DepositAmountFrom, string DepositAmountTo
            , string DepositReturnedFrom, string DepositReturnedTo
            //, string DepositRewardFrom, string DepositRewardTo //Atiqah 20190221 MDT-257
            , string DepositReturnedRewardFrom , string DepositReturnedRewardTo
            , string DepositOverflowRewardFrom , string DepositOverflowRewardTo
            , string ExtraRewardFrom, string ExtraRewardto
            , string TotalRewardFrom, string TotalRewardTo
            //Atiqah 20190221 MDT-257 \/
            //, string SelfScorePctFrom, string SelfScorePctTo
            //, string SelfScoreFrom, string SelfScoreTo
            //, string TeamScorePctFrom, string TeamScorePctTo
            //, string TeamScoreFrom, string TeamScoreTo
            //Atiqah 20190221 MDT-257 /\
            , out int TotalRecord, out int TotalPage
            , int pageIndex = 0, int pageSize = 1
            );//Atiqah 20180927 MSP-150

        /// <summary>
        /// Get topup by identifiers
        /// </summary>
        /// <param name="ids">Topup identifiers</param>
        /// <returns>topups</returns>
        //IList<MSP_Mbtc_Deposit> GetTopupByIds(int[] ids);

        IList<SelectListItem> GetWithdrawalStatusValue(); //WilliamHo 20190124

        IList<SelectListItem> GetTransactionStatusValue(); //WilliamHo 20190125
    }
}

