using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Msp;
using Nop.Core.Domain.Msp.Custom;
using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Setting;
using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Core.Enumeration;
using Nop.Data;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Nop.Core.Extensions;
using static Nop.Core.Domain.Msp.Custom.DepositTransactionCustom;

namespace Nop.Services.Transaction
{
    public partial class TransactionService : ITransactionService
    {
        private readonly IRepository<MSP_Mbtc_Deposit> _MSP_Mbtc_DepositRepository;
        private readonly IRepository<MSP_Mbtc_Withdrawal> _mspWithdrawal_Mbtc_WithdrawalRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IRepository<MSP_Wallet> _mspWalletRepository;
        private readonly IRepository<MSP_MemberTree> _mspMembertreeRepository;
        private readonly IRepository<MSP_Deposit> _MSP_DepositRepository;
        private readonly IDbContext _dbContext;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IMspHelper _mspHelper;
        private readonly IRepository<MSP_Consumption_Platform> _mspConsumptionPlatformRepository;
        private readonly ILocalizationService _localizationService;



        public TransactionService(
            IRepository<MSP_Mbtc_Deposit> MSP_Mbtc_DepositRepository,
            IRepository<MSP_Mbtc_Withdrawal> MSP_Mbtc_WithdrawalRepository,
            IRepository<Customer> CustomerRepository,
            IRepository<MSP_Consumption_Platform> MSP_Consumption_PlatformRepository,
            ICustomerService customerService,
            IRepository<MSP_Wallet> MSPWalletRepository,
            IRepository<MSP_MemberTree> MSPMemberTreeRepository,
            IRepository<MSP_Deposit> MSP_DepositRepository,
            IDateTimeHelper dateTimeHelper,
            IDbContext dbContext,
            ILocalizationService localizationService,
            IMspHelper mspHelper //Jerry 20181009 MSP-207

            )
        {
            this._MSP_Mbtc_DepositRepository = MSP_Mbtc_DepositRepository;
            this._mspWithdrawal_Mbtc_WithdrawalRepository = MSP_Mbtc_WithdrawalRepository;
            this._customerRepository = CustomerRepository;
            this._customerService = customerService;
            this._mspWalletRepository = MSPWalletRepository;
            this._mspMembertreeRepository = MSPMemberTreeRepository;
            this._mspConsumptionPlatformRepository = MSP_Consumption_PlatformRepository;
            this._MSP_DepositRepository = MSP_DepositRepository;
            this._dateTimeHelper = dateTimeHelper;
            this._dbContext = dbContext;
            this._localizationService = localizationService;
            this._mspHelper = mspHelper; //Jerry 20181009 MSP-207
        }

        #region Transaction Listing 
        /// <summary>
        /// GetStatus
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetWithdrawalStatusValue() //WilliamHo 20190124
        {
            var _statusValues = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource("Admin.TransactionList.Transaction.Status.All"), Value = " "},
            };

            var StatusAsList = Enum.GetValues(typeof(WithdrawalStatusEnum)).Cast<WithdrawalStatusEnum>().ToList();
            if (StatusAsList.Count > 0)
            {
                foreach (var status in StatusAsList)
                {
                    _statusValues.Add(new SelectListItem { Text = status.ToDescription<WithdrawalStatusEnum>(), Value = status.ToValue<WithdrawalStatusEnum>() });
                }
            }
            return _statusValues;
        }

        /// <summary>
        /// Generally used that status with New, Success, Failed
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetTransactionStatusValue() //WilliamHo 20190124
        {
            var _statusValues = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource("Admin.TransactionList.Transaction.Status.All"), Value = " "},
            };

            var StatusAsList = Enum.GetValues(typeof(MSP_Settlement_Status)).Cast<MSP_Settlement_Status>().ToList();
            if (StatusAsList.Count > 0)
            {
                foreach (var status in StatusAsList)
                {
                    _statusValues.Add(new SelectListItem { Text = status.ToDescription<MSP_Settlement_Status>(), Value = status.ToValue<MSP_Settlement_Status>() });
                }
            }
            return _statusValues;
        }

        #region MSP-46 Backend Function: Transaction Listing > Top Up //RW MSP-46 Top Up Listing  20180823
        public IPagedList<MSP_Mbtc_Deposit> GetTopUpDetails(string Username, DateTime? DateFrom, DateTime? DateTo, int? TxId, decimal? TopUpAmt, string TopUpAmtMBTCAdd, string BlockchainTxId, string Status, int pageIndex = 0, int pageSize = int.MaxValue) //wailiang 20190116 MDT-193
        {
            //IList<MSP_Mbtc_Deposit> List = new List<MSP_Mbtc_Deposit>();

            var CustomerId = _customerRepository.Table.Where(o => o.Username.Contains(Username)).Select(o => o.Id).ToArray(); //RW MSP-189 20181002

            //var BlockchainUrlPrefix = _mspHelper.GetBlockchainTxIdPrefixLink(); //wailiang 20190116 MDT-193

            var MSP_Mbtc_DepositRepo = _MSP_Mbtc_DepositRepository.Table.AsQueryable();

            #region filter
            if (!string.IsNullOrEmpty(Username)) //RW MSP-189 20181002
            {
                MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.Where(o => CustomerId.Contains(o.CustomerID));
            }

            if (DateFrom.HasValue)
            {
                MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.Where(o => o.CreatedOnUtc >= DateFrom);
            }
            if (DateTo.HasValue)
            {
                MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.Where(o => o.CreatedOnUtc < DateTo);
            }

            if (TxId.HasValue)
            {
                MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.Where(o => o.TxID == TxId);
            }

            if (TopUpAmt.HasValue)
            {
                MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.Where(o => o.MbtcAmt == TopUpAmt);
            }

            if (!string.IsNullOrEmpty(TopUpAmtMBTCAdd))
            {
                MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.Where(o => o.WalletAddress == TopUpAmtMBTCAdd);
            }

            if (!string.IsNullOrEmpty(Status))
            {
                MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.Where(o => o.Status == Status);
            }

            if (!string.IsNullOrEmpty(BlockchainTxId))
            {
                MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.Where(o => o.BlockChainTxId == BlockchainTxId);
            }

            MSP_Mbtc_DepositRepo = MSP_Mbtc_DepositRepo.OrderByDescending(o => o.CreatedOnUtc);

            #endregion

            //List = MSP_Mbtc_DepositRepo.ToList().Select(o => new MSP_Mbtc_Deposit
            // {
            //     CustomerID = o.CustomerID,
            //     TxID = o.TxID,
            //     MbtcAmt = o.MbtcAmt,
            //     WalletAddress = o.WalletAddress,
            //     CreatedOnUtc = o.CreatedOnUtc
            // }).ToList();

            //var topuplist = new PagedList<MSP_Mbtc_Deposit>(MSP_Mbtc_DepositRepo, pageIndex, pageSize);
            //return MSP_Mbtc_DepositRepo.ToList();

            //RW 20190928 MSP-46 \/
            var list = new PagedList<MSP_Mbtc_Deposit>(MSP_Mbtc_DepositRepo, pageIndex, pageSize);
            //StringBuilder sb = new StringBuilder();
            //wailiang 20190116 MDT-193 \/
            foreach (var item in list)
            {
                //if (!string.IsNullOrEmpty(item.BlockChainTxId))
                //{
                //    sb.Clear();
                //    item.BlockChainTxId = sb.AppendFormat("{0}{1}", BlockchainUrlPrefix, item.BlockChainTxId).ToString();
                //}
            };
            //wailiang 20190116 MDT-193 /\

            return list;
            //RW 20190928 MSP-46 /\
        }

        #endregion

        #region Consumption Reward History
        public virtual IPagedList<ConsumptionRewardHistoryCustom> ConsumptionRewardHistoryList(int customerId, DateTime date, int pageIndex = 0, int pageSize = int.MaxValue) //wailiang 20180921 MSP-98
        {
            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_ConsumptionRewardTeam]");
            sp.Append("@CustomerID");
            sp.Append(",@Date");

            var returnList = _dbContext.SqlQuery<ConsumptionRewardHistoryCustom>(
                sp.ToString()
                , new SqlParameter("@CustomerID", customerId)
                , new SqlParameter("@Date", date)
                ).ToList();
            
            var getreturn = new PagedList<ConsumptionRewardHistoryCustom>(returnList, pageIndex, pageSize);

            return getreturn;
        }
        #endregion

        #region Deposit
        public IPagedList<DepositTransactionCustom> GetDepositDetails(string username = null,
            DateTime? DateFrom = null, DateTime? DateTo = null, int? TxnNo = null, decimal? DepositAmt = null, string status = null, int pageIndex = 0, int pageSize = int.MaxValue) //Atiqah 20190903 MSP-94 //wailiang 20190115 MDT-194
        {
            //Atiqah 20181003 MSP-195 \/
            var query = from D in _MSP_DepositRepository.Table
                        join C in _customerRepository.Table
                        on D.CustomerID equals C.Id
                        where (C.Username.Contains(username) || username == null || username == "")
                        && D.CreatedOnUtc >= DateFrom && D.CreatedOnUtc < DateTo
                        select new DepositTransactionCustom
                        {
                            Username = C.Username,
                            Date = D.CreatedOnUtc,
                            TxnNo = D.Id,
                            DepositAmt = D.DepositAmt,
                            //wailiang 20190115 MDT-194 \/
                            Status = D.Status
                            //wailiang 20190115 MDT-194 /\
                        };
            //Atiqah 20181003 MSP-195 /\

            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);

            query = query.OrderByDescending(x => x.Date); //Atiqah 20181003 MSP-194

            //Tony Liew MSP-525 20181123 \/
            if (TxnNo.HasValue)
                query = query.Where(o => o.TxnNo == TxnNo);

            if (DepositAmt.HasValue)
                query = query.Where(o => o.DepositAmt == DepositAmt);
            //Tony Liew MSP-525 20181123 /\

            var pageList = new PagedList<DepositTransactionCustom>(query, pageIndex, pageSize);

            //wailiang 20190115 MDT-194 \/
            foreach (var item in pageList)
            {
                switch (item.Status)
                {
                    case "S": item.Status = "Success"; break;
                    case "F": item.Status = "Failed"; break;
                    default: item.Status = "New"; break;
                }
            };
            //wailiang 20190115 MDT-194 /\

            return pageList;
        }

		#region Comment Code
		//var CustomerID = _customerRepository.Table.Where(o => o.Username == username).Select(o => o.Id).FirstOrDefault();
		//var query = _MSP_DepositRepository.Table;

		//if (CustomerID > 0)
		//    query = query.Where(o => o.CustomerID == CustomerID);

		//if (DateFrom.HasValue)
		//    query = query.Where(o => o.CreatedOnUtc >= DateFrom.Value);

		//if (DateTo.HasValue)
		//    query = query.Where(o => o.CreatedOnUtc <= DateTo.Value);

		//if (TxnNo.HasValue)
		//    query = query.Where(o => o.Id == TxnNo);

		//if (DepositAmt.HasValue)
		//    query = query.Where(o => o.DepositAmt == DepositAmt);

		//query = query.OrderBy(o => o.CreatedOnUtc);

		//return query.ToList(); //Atiqah 20190925 MSP-94

		//Atiqah 20190925 MSP-94 \/
		//var Deposit = new PagedList<MSP_Deposit>(query, pageIndex, pageSize);
		//    return Deposit;
		//Atiqah 20190925 MSP-94 /\
		#endregion

		#region Comment Code

		///// <summary>
		///// Get topup by identifiers
		///// </summary>
		///// <param name="ids">Topup identifiers</param>
		///// <returns>topups</returns>
		//public virtual IList<MSP_Mbtc_Deposit> GetTopupByIds(int[] ids)  // 20181217 WKK MDT-153 MSP requirement 1.2 > Export function to CSV > Transaction Listing ? Top Up
		//{
		//	if (ids == null || ids.Length == 0)
		//		return new List<MSP_Mbtc_Deposit>();

		//	var query = from c in _MSP_Mbtc_DepositRepository.Table
		//				where ids.Contains(c.Id)
		//				select c;

		//	var list = query.ToList();

		//	//sort by passed identifiers
		//	var sorted = new List<MSP_Mbtc_Deposit>();
		//	foreach (var id in ids)
		//	{
		//		var tran = list.Find(x => x.Id == id);
		//		if (tran != null)
		//			sorted.Add(tran);
		//	}

		//	return sorted;
		//}
		#endregion

		#endregion

		#region Withdrawal
		public IPagedList<WithdrawalTransactionCustom> GetAllWithdrawal(string username = null, int? _txnNo = null, decimal? _amt = null, decimal? _txnFees = null, decimal? _netAmt = null, string _withdrawalAddress = null,
            string _blockchainTxId = null, string _status = null, DateTime? DateFrom = null, DateTime? DateTo = null, int pageIndex = 0, int pageSize = int.MaxValue) //wailiang 20190910 MSP-95 //wailiang 20190116 MDT-195
        {
            //wailiang 20181003 MSP-195 \/
            //var CustomerID = _customerRepository.Table.Where(o => o.Username == username).Select(o => o.Id).FirstOrDefault();
            //var query = _mspWithdrawal_Mbtc_WithdrawalRepository.Table;

            //var BlockchainUrlPrefix = _mspHelper.GetBlockchainTxIdPrefixLink();

            var query = from w in _mspWithdrawal_Mbtc_WithdrawalRepository.Table
                        join c in _customerRepository.Table
                        on w.CustomerID equals c.Id
                        where (c.Username.Contains(username) || username == null || username == "")
                        select new WithdrawalTransactionCustom
                        {
                            Username = c.Username,
                            Date = w.CreatedOnUtc,
                            TxnNo = w.Id,
                            Amount = w.WithdrawAmt,
                            TransactionFees = w.TxFee,
                            NetAmount = w.NetWithdrawAmt,
                            //wailiang 20190116 MDT-195 \/
                            WithdrawalAddress = w.WalletAddress,
                            BlockChainTxId = w.BlockChainTxId,
                            Status = w.Status,
                            RefundStatus = w.RefundProcessStatus,
                            Remark = w.ErrorMessage
                            //wailiang 20190116 MDT-195 /\
                        };

            //if (CustomerID > 0)
            //    query = query.Where(o => o.CustomerID == CustomerID);

            if (DateFrom.HasValue) //wailiang 20181031 MSP-423
            {
                var fromDate = _dateTimeHelper.ConvertToUtcTime(DateFrom.Value);
                query = query.Where(x => x.Date >= fromDate);
            }

            if (DateTo.HasValue) //wailiang 20181031 MSP-423
            {
                var toDate = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(DateTo.Value));
                query = query.Where(x => x.Date <= toDate);
            }

            //Tony Liew MSP-526 20181123 \/
            if (_txnNo.HasValue)
                query = query.Where(o => o.TxnNo == _txnNo);

            if (_amt.HasValue)
                query = query.Where(o => o.Amount == _amt);

            if (_txnFees.HasValue)
                query = query.Where(o => o.TransactionFees == _txnFees);

            if (_netAmt.HasValue)
                query = query.Where(o => o.NetAmount == _netAmt);

            //Tony Liew MSP-526 20181123 /\

            //WilliamHo 20190124 MDT-195 \/
            if (!string.IsNullOrEmpty(_withdrawalAddress))
                query = query.Where(o => o.WithdrawalAddress == _withdrawalAddress);

            if (!string.IsNullOrEmpty(_blockchainTxId))
                query = query.Where(o => o.BlockChainTxId == _blockchainTxId);

            if (!string.IsNullOrEmpty(_status))
            {
                if (_status == "N")
                {
                    query = query.Where(o => o.Status == "N");
                }
                else if(_status == "P") //Pending and New
                {
                    query = query.Where(o => (o.Status == "S" && o.BlockChainTxId == null) && !(o.Status == "F" || o.RefundStatus != null || o.Remark != null));
                }
                else if(_status == "F") //Failed
                {
                    query = query.Where(o => o.Status == "F" || o.RefundStatus != null || o.Remark != null);
                }
                else //Success
                {
                    query = query.Where(o => o.Status == _status && o.BlockChainTxId != null && (o.RefundStatus == null && o.Remark == null));
                }
            }
            //WilliamHo 20190124 MDT-195 /\

            #region comment code
            //if (_txnNo.HasValue)
            //{
            //    query = query.Where(o => o.Id == _txnNo);
            //}

            //if (_amt.HasValue)
            //{
            //    //query = query.Where(o => o.WithdrawAmt >= _amt); //WilliamHo 20181002 get the exact value
            //    query = query.Where(o => o.WithdrawAmt == _amt);
            //}

            //if (_txnFees.HasValue)
            //{
            //    //query = query.Where(o => o.TxFee >= _txnFees); //WilliamHo 20181002 get the exact value
            //    query = query.Where(o => o.TxFee == _txnFees);
            //}

            //if (_netAmt.HasValue)
            //{
            //    //query = query.Where(o => o.NetWithdrawAmt >= _netAmt); //WilliamHo 20181002 get the exact value
            //    query = query.Where(o => o.NetWithdrawAmt == _netAmt);
            //}
            #endregion
            //wailiang 20181003 MSP-195 /\
            query = query.OrderByDescending(o => o.Date);

            //wailiang 20180926 MSP-95 \/

            var Withdrawal = new PagedList<WithdrawalTransactionCustom>(query, pageIndex, pageSize);

            //wailiang 20190116 MDT-195 \/
            //StringBuilder sb = new StringBuilder();

            foreach (var item in Withdrawal)
            {
                if (item.Status.EqualsIgnoreCase("F"))
                {
                    item.Status = "Failed";
                    item.Remark = "";
                }
                else if (!string.IsNullOrEmpty(item.RefundStatus))
                {
                    item.Status = "Failed";
                    if (item.RefundStatus.EqualsIgnoreCase("S"))
                    {
                        item.RefundStatus = "Success";
                    }
                    else
                    {
                        item.RefundStatus = "Failed";
                    }
                }
                else if (!string.IsNullOrEmpty(item.Remark))
                {
                    item.Status = "Failed";
                }
                else if (item.Status.EqualsIgnoreCase("S") && string.IsNullOrEmpty(item.BlockChainTxId)) //wailiang 20190123 MDT-195
                {
                    item.Status = "Pending";
                }
                else if (item.Status.EqualsIgnoreCase("N"))
                {
                    item.Status = "New";
                }
                else
                {
                    item.Status = "Success";
                }
                
                //sb.Clear();
                //if (!string.IsNullOrEmpty(item.BlockChainTxId))
                //    item.BlockChainTxId = sb.AppendFormat("{0}{1}", BlockchainUrlPrefix, item.BlockChainTxId).ToString();
            }
            //wailiang 20190116 MDT-195 /\

            return Withdrawal;
            //return query.ToList();
            //wailiang 20180926 MSP-95 /\

        }
        #endregion

        #region Redeem
        public virtual IPagedList<RedeemTransactionsCustom> GetAllRedeemTransactions(string username = null, string walletid = null,
            string availableBalanceFrom = null, string availableBalanceTo = null, string redeemWalletBalanceFrom = null, string redeemWalletBalanceTo = null,
            int pageIndex = 0, int pageSize = 1) //JK 20180912 MSP-96
        {
            decimal? availableBalFrom = null;
            decimal? availableBalTo = null;
            decimal? redeemWalletBalFrom = null;
            decimal? redeemWalletBalTo = null;

            if (!string.IsNullOrEmpty(availableBalanceFrom))
            {
                decimal.TryParse(availableBalanceFrom, out decimal savailableBalFrom);
                availableBalFrom = savailableBalFrom;
            }
            if (!string.IsNullOrEmpty(availableBalanceTo))
            {
                decimal.TryParse(availableBalanceTo, out decimal savailableBalTo);
                availableBalTo = savailableBalTo;
            }
            if (!string.IsNullOrEmpty(redeemWalletBalanceFrom))
            {
                decimal.TryParse(redeemWalletBalanceFrom, out decimal sredeemWalletBalFrom);
                redeemWalletBalFrom = sredeemWalletBalFrom;
            }
            if (!string.IsNullOrEmpty(redeemWalletBalanceTo))
            {
                decimal.TryParse(redeemWalletBalanceTo, out decimal sredeemWalletBalanceTo);
                redeemWalletBalTo = sredeemWalletBalanceTo;
            }

            // var CustomerID = _customerRepository.Table.Where(o => o.Username == username).Select(o => o.Id).FirstOrDefault();
            var query = (from C in _customerRepository.Table
                         join MW in _mspWalletRepository.Table on C.Id equals MW.CustomerID
                         join MMT in _mspMembertreeRepository.Table on C.Id equals MMT.CustomerID
                         where (C.Username.Contains(username) || username == null || username == "")
                         &&
                         (
                         (availableBalTo != null && availableBalFrom != null && MW.Mbtc >= availableBalFrom && MW.Mbtc <= availableBalTo) ||
                         (availableBalTo != null && availableBalFrom == null && MW.Mbtc <= availableBalTo) ||
                         (availableBalTo == null && availableBalFrom != null && MW.Mbtc >= availableBalFrom) ||
                         (availableBalFrom == null && availableBalTo == null)
                         )
                         &&
                         (
                         (redeemWalletBalTo != null && redeemWalletBalFrom != null && MW.Profit_DP_Float >= redeemWalletBalFrom && MW.Profit_DP_Float <= redeemWalletBalTo) ||
                         (redeemWalletBalTo != null && redeemWalletBalFrom == null && MW.Profit_DP_Float <= redeemWalletBalTo) ||
                         (redeemWalletBalTo == null && redeemWalletBalFrom != null && MW.Profit_DP_Float >= redeemWalletBalFrom) ||
                         (redeemWalletBalFrom == null && redeemWalletBalTo == null)
                         )

                         select new RedeemTransactionsCustom
                         {
                             CustomerID = C.Id,
                             Username = C.Username,
                             WalletID = null,
                             AvailableBalance = MW.Mbtc,
                             RedeemWalletBalance = MW.Profit_DP_Float
                         });

            //if (CustomerID > 0)
            //    query = query.Where(o => o.CustomerID == CustomerID);

            //if (!string.IsNullOrEmpty(availableBalanceFrom) && decimal.TryParse(availableBalanceFrom, out decimal availableBalFrom))
            //    query = query.Where(x => x.AvailableBalance >= availableBalFrom);

            //if (!string.IsNullOrEmpty(availableBalanceTo) && decimal.TryParse(availableBalanceTo, out decimal availableBalTo))
            //    query = query.Where(x => x.AvailableBalance <= availableBalTo);

            //if (!string.IsNullOrEmpty(redeemWalletBalanceFrom) && decimal.TryParse(redeemWalletBalanceFrom, out decimal redeemWalletBalFrom))
            //    query = query.Where(x => x.RedeemWalletBalance >= redeemWalletBalFrom);

            //if (!string.IsNullOrEmpty(redeemWalletBalanceTo) && decimal.TryParse(redeemWalletBalanceTo, out decimal redeemWalletBalTo))
            //    query = query.Where(x => x.RedeemWalletBalance <= redeemWalletBalTo);

            query = query.OrderBy(x => x.Username);

            var pageList = new PagedList<RedeemTransactionsCustom>(query, pageIndex, pageSize);

            return pageList;
        }
        #endregion

        #region Consumption Reward Self
        /// <summary>
        /// Populate platform name list for Consumption Reward Self
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetPlatformNameValue() //JK 20180926 MSP-97
        {
            var _publishValues = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource("Admin.TransactionList.PlatformName.All"), Value = "0"},
                new SelectListItem { Text = _localizationService.GetResource("Admin.TransactionList.PlatformName.MSP"), Value = "-1"},
            };

            var platforms = (from t in _mspConsumptionPlatformRepository.Table
                             orderby t.PlatformID
                             select t).ToList();
            if (platforms.Count > 0)
            {
                foreach (var plaform in platforms)
                {
                    _publishValues.Add(new SelectListItem { Text = plaform.PlatformName, Value = plaform.PlatformID.ToString() });
                }
            }
            return _publishValues;
        }

        public List<ConsumptionRewardSelfCustom> GetConsumptionRewardSelfList(DateTime? FromDate, DateTime? ToDate, string username, int platformid,
            string DepositReturnFrom, string DepositReturnTo, string MemberRewardFrom, string MemberRewardTo, string ConsumptionFrom,
            string ConsumptionTo, /*string MerchantRefFrom, string MerchantRefTo,*/ string TotalRewardFrom, string TotalRewardTo) //JK 20180926 MSP-97 //wailiang 20181003 MSP-97 //Atiqah 20190219 MDT-244
        {
            #region Declaration
            //Atiqah 20181023 MSP-364 \/
            decimal? DepositRtnFrom = _mspHelper.ConvertStringToDecimal(DepositReturnFrom);
            decimal? DepositRtnTo = null; /*_mspHelper.ConvertStringToDecimal(DepositReturnTo);*/ //Atiqah 20190219 MDT-244
            decimal? MemberRwdFrom = _mspHelper.ConvertStringToDecimal(MemberRewardFrom);
            decimal? MemberRwdTo = null; /*_mspHelper.ConvertStringToDecimal(MemberRewardTo);*/ //Atiqah 20190219 MDT-244
            decimal? ConsumpFrom = _mspHelper.ConvertStringToDecimal(ConsumptionFrom);
            decimal? ConsumpTo = null;/*_mspHelper.ConvertStringToDecimal(ConsumptionTo);*/ //Atiqah 20190219 MDT-244
            //decimal? MercntRefFrom = _mspHelper.ConvertStringToDecimal(MerchantRefFrom); //Atiqah 20190219 MDT-244
            //decimal? MercntRefTo = _mspHelper.ConvertStringToDecimal(MerchantRefTo); //Atiqah 20190219 MDT-244
            decimal ? TotalRwdFrom = _mspHelper.ConvertStringToDecimal(TotalRewardFrom);
            decimal? TotalRwdTo = null;/*_mspHelper.ConvertStringToDecimal(TotalRewardTo);*/ //Atiqah 20190219 MDT-244
            //Atiqah 20181023 MSP-364 /\

            //Chew 20190125 MSP-695 \/
            //TotalRecord = 0;
            //TotalPage = 1;
            //Chew 20190125 MSP-695 /\
            #endregion Declaration

            //Chew 20190125 MSP-695 \/
            #region Validation
            #region Commented Code
            //if (!string.IsNullOrEmpty(DepositReturnFrom))
            //{
            //    DepositRtnFrom = _mspHelper.ConvertStringToDecimal(DepositReturnFrom);
            //}
            //if (!string.IsNullOrEmpty(DepositReturnTo))
            //{
            //    DepositRtnTo = _mspHelper.ConvertStringToDecimal(DepositReturnTo);
            //}
            //if (!string.IsNullOrEmpty(MemberRewardFrom))
            //{
            //    MemberRwdFrom = _mspHelper.ConvertStringToDecimal(MemberRewardFrom);
            //}
            //if (!string.IsNullOrEmpty(MemberRewardTo))
            //{
            //    MemberRwdTo = _mspHelper.ConvertStringToDecimal(MemberRewardTo);
            //}
            //if (!string.IsNullOrEmpty(ConsumptionFrom))
            //{
            //    ConsumpFrom = _mspHelper.ConvertStringToDecimal(ConsumptionFrom);
            //}
            //if (!string.IsNullOrEmpty(ConsumptionTo))
            //{
            //    ConsumpTo = _mspHelper.ConvertStringToDecimal(ConsumptionTo);
            //}
            //if (!string.IsNullOrEmpty(MerchantRefFrom))
            //{
            //    MercntRefFrom = _mspHelper.ConvertStringToDecimal(MerchantRefFrom);
            //}
            //if (!string.IsNullOrEmpty(MerchantRefTo))
            //{
            //    MercntRefTo = _mspHelper.ConvertStringToDecimal(MerchantRefTo);
            //}
            //if (!string.IsNullOrEmpty(TotalRewardFrom))
            //{
            //    TotalRwdFrom = _mspHelper.ConvertStringToDecimal(TotalRewardFrom);
            //}
            //if (!string.IsNullOrEmpty(TotalRewardTo))
            //{
            //    TotalRwdTo = _mspHelper.ConvertStringToDecimal(TotalRewardTo);
            //}
            #endregion Commented Code

            //Atiqah 20190219 MDT-244 \/
            if (!string.IsNullOrEmpty(DepositReturnTo))
            {
                if (decimal.TryParse(DepositReturnTo, out decimal result))
                    DepositRtnTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(MemberRewardTo))
            {
                if (decimal.TryParse(MemberRewardTo, out decimal result))
                    MemberRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(ConsumptionTo))
            {
                if (decimal.TryParse(ConsumptionTo, out decimal result))
                    ConsumpTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(TotalRewardTo))
            {
                if (decimal.TryParse(TotalRewardTo, out decimal result))
                    TotalRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }
            //Atiqah 20190219 MDT-244 /\
            #endregion Validation
            //Chew 20190125 MSP-695 /\

            //wailiang 20181003 MSP-97 \/
            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_ConsumptionRewardSelf]");
            sp.Append(" @Username");
            sp.Append(",@PlatformID");
            sp.Append(",@FromDate");
            sp.Append(",@ToDate");
            sp.Append(",@DepositReturnFrom");
            sp.Append(",@DepositReturnTo");
            sp.Append(",@MemberRewardFrom");
            sp.Append(",@MemberRewardTo");
            sp.Append(",@ConsumptionFrom");
            sp.Append(",@ConsumptionTo");
            //sp.Append(",@MerchantRefFrom"); //Atiqah 20190219 MDT-244
            //sp.Append(",@MerchantRefTo"); //Atiqah 20190219 MDT-244
            sp.Append(",@TotalRewardFrom");
            sp.Append(",@TotalRewardTo");
            //Chew 20190125 MSP-695 \/
            //sp.Append(",@PageNumber");
            //sp.Append(",@PageSize");
            //sp.Append(",@outTotalRecords OUTPUT");
            //sp.Append(",@outTotalPages OUTPUT");
            //Chew 20190125 MSP-695 /\

            //Chew 20190125 MSP-695 \/
            #region Output Variable
            //SqlParameter pTotalRecords = new SqlParameter();
            //pTotalRecords.ParameterName = "@outTotalRecords";
            //pTotalRecords.DbType = DbType.Int32;
            //pTotalRecords.Direction = ParameterDirection.Output;

            //SqlParameter pTotalPages = new SqlParameter();
            //pTotalPages.ParameterName = "@outTotalPages";
            //pTotalPages.DbType = DbType.Int32;
            //pTotalPages.Direction = ParameterDirection.Output;
            #endregion Output Variable
            //Chew 20190125 MSP-695 /\

            var returnList = _dbContext.SqlQuery<ConsumptionRewardSelfCustom>(
                sp.ToString()
                , new SqlParameter("@Username", username ?? (object)DBNull.Value)
                , new SqlParameter("@PlatformID", platformid)
                , new SqlParameter("@FromDate", FromDate)
                , new SqlParameter("@ToDate", ToDate)
                , new SqlParameter("@DepositReturnFrom", DepositRtnFrom ?? (object)DBNull.Value)
                , new SqlParameter("@DepositReturnTo", DepositRtnTo ?? (object)DBNull.Value)
                , new SqlParameter("@MemberRewardFrom", MemberRwdFrom ?? (object)DBNull.Value)
                , new SqlParameter("@MemberRewardTo", MemberRwdTo ?? (object)DBNull.Value)
                , new SqlParameter("@ConsumptionFrom", ConsumpFrom ?? (object)DBNull.Value)
                , new SqlParameter("@ConsumptionTo", ConsumpTo ?? (object)DBNull.Value)
                //, new SqlParameter("@MerchantRefFrom", MercntRefFrom ?? (object)DBNull.Value) //Atiqah 20190219 MDT-244
                //, new SqlParameter("@MerchantRefTo", MercntRefTo ?? (object)DBNull.Value) //Atiqah 20190219 MDT-244
                , new SqlParameter("@TotalRewardFrom", TotalRwdFrom ?? (object)DBNull.Value)
                , new SqlParameter("@TotalRewardTo", TotalRwdTo ?? (object)DBNull.Value)
                //Chew 20190125 MSP-695 \/
                //, new SqlParameter("@PageNumber", pageIndex)
                //, new SqlParameter("@PageSize", pageSize)
                //, pTotalRecords
                //, pTotalPages
                //Chew 20190125 MSP-695 /\
                ).ToList();

            //Chew 20190125 MSP-695 \/
            //if (returnList.Count > 0) //Atiqah 20181023 MSP-364 
            //{
            //    TotalPage = Convert.ToInt32(pTotalPages.SqlValue.ToString());
            //    TotalRecord = Convert.ToInt32(pTotalRecords.SqlValue.ToString());
            //}
            //Chew 20190125 MSP-695 /\

            return returnList;//Atiqah 20181023 MSP-364 

            //var consumptionRewardSelfHistory = new PagedList<ConsumptionRewardSelfCustom>(returnList, pageIndex, pageSize);

            //return consumptionRewardSelfHistory;

            //wailiang 20181003 MSP-97 /\
        }
        #endregion


        // 20190222 WKK MDT-248 Admin Panel > Team Task Reward
        #region Consumption Reward Team

        public List<ConsumptionRewardSelfCustom> GetConsumptionRewardTeamList(DateTime? FromDate, DateTime? ToDate, string username, int platformid,
            string DepositReturnFrom, string DepositReturnTo, string MemberRewardFrom, string MemberRewardTo, string ConsumptionFrom,
            string ConsumptionTo, string HonoraryCitizenRewardFrom, string HonoraryCitizenRewardTo, string TotalRewardFrom, string TotalRewardTo) 
        {
            #region Declaration
            decimal? DepositRtnFrom = _mspHelper.ConvertStringToDecimal(DepositReturnFrom);
            decimal? DepositRtnTo = null; /*_mspHelper.ConvertStringToDecimal(DepositReturnTo);*/ //Atiqah 20190219 MDT-244
            decimal? MemberRwdFrom = _mspHelper.ConvertStringToDecimal(MemberRewardFrom);
            decimal? MemberRwdTo = null; /*_mspHelper.ConvertStringToDecimal(MemberRewardTo);*/ //Atiqah 20190219 MDT-244
            decimal? ConsumpFrom = _mspHelper.ConvertStringToDecimal(ConsumptionFrom);
            decimal? ConsumpTo = null;/*_mspHelper.ConvertStringToDecimal(ConsumptionTo);*/ //Atiqah 20190219 MDT-244
            decimal? TotalRwdFrom = _mspHelper.ConvertStringToDecimal(TotalRewardFrom);
            decimal? TotalRwdTo = null;/*_mspHelper.ConvertStringToDecimal(TotalRewardTo);*/ //Atiqah 20190219 MDT-244
            decimal? HonoraryCitizenRwdFrom = _mspHelper.ConvertStringToDecimal(HonoraryCitizenRewardFrom);
            decimal? HonoraryCitizenRwdTo = null;
            #endregion Declaration

            if (!string.IsNullOrEmpty(DepositReturnTo))
            {
                if (decimal.TryParse(DepositReturnTo, out decimal result))
                    DepositRtnTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(MemberRewardTo))
            {
                if (decimal.TryParse(MemberRewardTo, out decimal result))
                    MemberRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(ConsumptionTo))
            {
                if (decimal.TryParse(ConsumptionTo, out decimal result))
                    ConsumpTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(TotalRewardTo))
            {
                if (decimal.TryParse(TotalRewardTo, out decimal result))
                    TotalRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(HonoraryCitizenRewardTo))
            {
                if (decimal.TryParse(HonoraryCitizenRewardTo, out decimal result))
                    HonoraryCitizenRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_ConsumptionRewardTeamAllUser]");
            sp.Append(" @Username");
            sp.Append(",@DateFrom");
            sp.Append(",@DateTo");
            sp.Append(",@PlatformId");
            sp.Append(",@TotalDepositReturnFrom");
            sp.Append(",@TotalDepositReturnTo");
            sp.Append(",@TotalMembershipRewardFrom");
            sp.Append(",@TotalMembershipRewardTo");
            sp.Append(",@TotalConsumptionRewardFrom");
            sp.Append(",@TotalConsumptionRewardTo");
            sp.Append(",@TotalHonoraryCitizenRewardFrom");
            sp.Append(",@TotalHonoraryCitizenRewardTo");
            sp.Append(",@GrandTotalRewardFrom");
            sp.Append(",@GrandTotalRewardTo");

            var returnList = _dbContext.SqlQuery<ConsumptionRewardSelfCustom>(
                sp.ToString()
                , new SqlParameter("@Username", username ?? (object)DBNull.Value)
                , new SqlParameter("@DateFrom", FromDate)
                , new SqlParameter("@DateTo", ToDate)
                , new SqlParameter("@PlatformId", platformid)
                , new SqlParameter("@TotalDepositReturnFrom", DepositRtnFrom ?? (object)DBNull.Value)
                , new SqlParameter("@TotalDepositReturnTo", DepositRtnTo ?? (object)DBNull.Value)
                , new SqlParameter("@TotalMembershipRewardFrom", MemberRwdFrom ?? (object)DBNull.Value)
                , new SqlParameter("@TotalMembershipRewardTo", MemberRwdTo ?? (object)DBNull.Value)
                , new SqlParameter("@TotalConsumptionRewardFrom", ConsumpFrom ?? (object)DBNull.Value)
                , new SqlParameter("@TotalConsumptionRewardTo", ConsumpTo ?? (object)DBNull.Value)
                , new SqlParameter("@TotalHonoraryCitizenRewardFrom", HonoraryCitizenRwdFrom ?? (object)DBNull.Value)
                , new SqlParameter("@TotalHonoraryCitizenRewardTo", HonoraryCitizenRwdTo ?? (object)DBNull.Value)
                , new SqlParameter("@GrandTotalRewardFrom", TotalRwdFrom ?? (object)DBNull.Value)
                , new SqlParameter("@GrandTotalRewardTo", TotalRwdTo ?? (object)DBNull.Value)
                ).ToList();

            return returnList;
        }
        #endregion

        //WKK 20190219 MDT-246 Admin Panel > Merchant Referral Reward
        #region Consumption Reward Merchant Referral

        public List<ConsumptionRewardSelfCustom> GetConsumptionRewardMerchantReferralList(DateTime? FromDate, DateTime? ToDate, string username, int platformid,
            string MerchantRefFrom, string MerchantRefTo) 
        {
            #region Declaration
            decimal? MercntRefFrom = _mspHelper.ConvertStringToDecimal(MerchantRefFrom); 
            decimal? MercntRefTo = _mspHelper.ConvertStringToDecimal(MerchantRefTo); 

            #endregion Declaration

            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_ConsumptionRewardMerchantReferral]");
            sp.Append(" @Username");
            sp.Append(",@PlatformID");
            sp.Append(",@FromDate");
            sp.Append(",@ToDate");
            sp.Append(",@MerchantRefFrom"); 
            sp.Append(",@MerchantRefTo"); 

            var returnList = _dbContext.SqlQuery<ConsumptionRewardSelfCustom>(
                sp.ToString()
                , new SqlParameter("@Username", username ?? (object)DBNull.Value)
                , new SqlParameter("@PlatformID", platformid)
                , new SqlParameter("@FromDate", FromDate)
                , new SqlParameter("@ToDate", ToDate)
                , new SqlParameter("@MerchantRefFrom", MercntRefFrom ?? (object)DBNull.Value) 
                , new SqlParameter("@MerchantRefTo", MercntRefTo ?? (object)DBNull.Value) 

                ).ToList();


            return returnList;

        }
        #endregion

        #region Deposit Reward History
        public IList<DepositRewardHistoryCustom> GetDepositRewardList(
            DateTime? DateFrom
            , DateTime? DateTo
            , string Username
            //, string ContributedBy //Jerry 20181009 MSP-209 //20190221 MDT-257
            , string DepositAmountFrom
            , string DepositAmountTo
            , string DepositReturnedFrom
            , string DepositReturnedTo
            //, string DepositRewardFrom        //20190221 MDT-257
            //, string DepositRewardTo          //20190221 MDT-257
            , string DepositReturnedRewardFrom
            , string DepositReturnedRewardTo
            , string DepositOverflowRewardFrom
            , string DepositOverflowRewardTo
            , string ExtraRewardFrom
            , string ExtraRewardto
            , string TotalRewardFrom
            , string TotalRewardTo
            //, string SelfScorePctFrom         //20190221 MDT-257
            //, string SelfScorePctTo           //20190221 MDT-257
            //, string SelfScoreFrom            //20190221 MDT-257
            //, string SelfScoreTo              //20190221 MDT-257
            //, string TeamScorePctFrom         //20190221 MDT-257
            //, string TeamScorePctTo           //20190221 MDT-257
            //, string TeamScoreFrom            //20190221 MDT-257
            //, string TeamScoreTo              //20190221 MDT-257
            , out int TotalRecord
            , out int TotalPage
            , int pageNumber = 0
            , int pageSize = 1
            ) //Atiqah 20180927 MSP-150
        {
            #region Declaration

            decimal? DepositAmtFrom = null;
            decimal? DepositAmtTo = null;
            decimal? DepositRtnFrom = null;
            decimal? DepositRtnTo = null;
            //decimal? DepositRwdFrom = null;       //20190221 MDT-257
            //decimal? DepositRwdTo = null;         //20190221 MDT-257
            decimal? DepositRtnRwdFrom = null;      //20190221 MDT-257 
            decimal? DepositRtnRwdTo = null;        //20190221 MDT-257
            decimal? DepositOverflowRwdFrom = null; //20190221 MDT-257
            decimal? DepositOverflowRwdTo = null;   //20190221 MDT-257
            decimal? ExtraRwdFrom = null;           
            decimal? ExtraRwdTo = null;             
            decimal? TotalRwdFrom = null;           
            decimal? TotalRwdTo = null;             
            //decimal? SelfScore_PctFrom = null;    //20190221 MDT-257
            //decimal? SelfScore_PctTo = null;      //20190221 MDT-257
            //decimal? SelfScore_From = null;       //20190221 MDT-257
            //decimal? SelfScore_To = null;         //20190221 MDT-257
            //decimal? TeamScore_PctFrom = null;    //20190221 MDT-257
            //decimal? TeamScore_PctTo = null;      //20190221 MDT-257
            //decimal? TeamScore_From = null;       //20190221 MDT-257
            //decimal? TeamScore_To = null;         //20190221 MDT-257

            TotalRecord = 0;
            TotalPage = 1;

            #endregion Declaration

            #region Output Variable
            SqlParameter outReturnCode = new SqlParameter()
            {
                ParameterName = "@outReturnCode",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };

            SqlParameter outReturnMessage = new SqlParameter()
            {
                ParameterName = "@outReturnMessage",
                SqlDbType = SqlDbType.NVarChar,
                Size = 200,
                Direction = ParameterDirection.Output,
                Value = 0
            };

            SqlParameter outTotalRewardEarned = new SqlParameter()
            {
                ParameterName = "@outTotalRewardEarned",
                SqlDbType = SqlDbType.Decimal,
                Precision = 24,
                Scale = 10,
                Size = 200,
                Direction = ParameterDirection.Output,
                Value = 0
            };

            SqlParameter outTotalRecords = new SqlParameter()
            {
                ParameterName = "@outTotalRecords",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };

            SqlParameter outTotalPages = new SqlParameter()
            {
                ParameterName = "@outTotalPages",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };
            #endregion

            #region Validation
            if (!string.IsNullOrEmpty(DepositAmountFrom))
            {
                DepositAmtFrom = _mspHelper.ConvertStringToDecimal(DepositAmountFrom);
            }

            if (!string.IsNullOrEmpty(DepositAmountTo))
            {
                //DepositAmtTo = _mspHelper.ConvertStringToDecimal(DepositAmountTo);
                if (decimal.TryParse(DepositAmountTo, out decimal result))
                    DepositAmtTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(DepositReturnedFrom))
            {
                DepositRtnFrom = _mspHelper.ConvertStringToDecimal(DepositReturnedFrom);
            }

            if (!string.IsNullOrEmpty(DepositReturnedTo))
            {
                //DepositRtnTo = _mspHelper.ConvertStringToDecimal(DepositReturnedTo);
                if (decimal.TryParse(DepositReturnedTo, out decimal result))
                    DepositRtnTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(DepositReturnedRewardFrom))
            {
                DepositRtnRwdFrom = _mspHelper.ConvertStringToDecimal(DepositReturnedRewardFrom);
            }

            if (!string.IsNullOrEmpty(DepositReturnedRewardTo))
            {
                if (decimal.TryParse(DepositReturnedRewardTo, out decimal result))
                    DepositRtnRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(DepositOverflowRewardFrom))
            {
                DepositOverflowRwdFrom = _mspHelper.ConvertStringToDecimal(DepositOverflowRewardFrom);
            }

            if (!string.IsNullOrEmpty(DepositOverflowRewardTo))
            {
                if (decimal.TryParse(DepositOverflowRewardTo, out decimal result))
                    DepositOverflowRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(ExtraRewardFrom))
            {
                ExtraRwdFrom = _mspHelper.ConvertStringToDecimal(ExtraRewardFrom);
            }

            if (!string.IsNullOrEmpty(ExtraRewardto))
            {
                //ExtraRwdTo = _mspHelper.ConvertStringToDecimal(ExtraRewardto);
                if (decimal.TryParse(ExtraRewardto, out decimal result))
                    ExtraRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            if (!string.IsNullOrEmpty(TotalRewardFrom))
            {
                TotalRwdFrom = _mspHelper.ConvertStringToDecimal(TotalRewardFrom);
            }

            if (!string.IsNullOrEmpty(TotalRewardTo))
            {
                //TotalRwdTo = _mspHelper.ConvertStringToDecimal(TotalRewardTo);
                if (decimal.TryParse(TotalRewardTo, out decimal result))
                    TotalRwdTo = _mspHelper.TruncateDecimal_MBTC(result) + 0.00000099m;
            }

            #region Commented Code
            //if (!string.IsNullOrEmpty(DepositRewardFrom))
            //{
            //    DepositRwdFrom = _mspHelper.ConvertStringToDecimal(DepositRewardFrom);
            //}

            //if (!string.IsNullOrEmpty(DepositRewardTo))
            //{
            //    DepositRwdTo = _mspHelper.ConvertStringToDecimal(DepositRewardTo);
            //}

            //if (!string.IsNullOrEmpty(SelfScorePctFrom))
            //{
            //    SelfScore_PctFrom = _mspHelper.ConvertStringToDecimal(SelfScorePctFrom);
            //}

            //if (!string.IsNullOrEmpty(SelfScorePctTo))
            //{
            //    SelfScore_PctTo = _mspHelper.ConvertStringToDecimal(SelfScorePctTo);
            //}

            //if (!string.IsNullOrEmpty(SelfScoreFrom))
            //{
            //    SelfScore_From = _mspHelper.ConvertStringToDecimal(SelfScoreFrom);
            //}

            //if (!string.IsNullOrEmpty(SelfScoreTo))
            //{
            //    SelfScore_To = _mspHelper.ConvertStringToDecimal(SelfScoreTo);
            //}

            //if (!string.IsNullOrEmpty(TeamScorePctFrom))
            //{
            //    TeamScore_PctFrom = _mspHelper.ConvertStringToDecimal(TeamScorePctFrom);
            //}

            //if (!string.IsNullOrEmpty(TeamScorePctTo))
            //{
            //    TeamScore_PctTo = _mspHelper.ConvertStringToDecimal(TeamScorePctTo);
            //}

            //if (!string.IsNullOrEmpty(TeamScoreFrom))
            //{
            //    TeamScore_From = _mspHelper.ConvertStringToDecimal(TeamScoreFrom);
            //}

            //if (!string.IsNullOrEmpty(TeamScoreTo))
            //{
            //    TeamScore_To = _mspHelper.ConvertStringToDecimal(TeamScoreTo);
            //}
            #endregion Commented Code
            #endregion

            #region Prepare Stored Procedure
            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_DepositRewardHistory]");
            sp.Append(" @Username");
            sp.Append(",@DateFrom");
            sp.Append(",@DateTo");
            //sp.Append(",@ContributedBy"); //Jerry 20181009 MSP-209
            sp.Append(",@DepositAmountFrom");
            sp.Append(",@DepositAmountTo");
            sp.Append(",@DepositReturnedFrom");
            sp.Append(",@DepositReturnedTo");
            //sp.Append(",@DepositRewardFrom");
            //sp.Append(",@DepositRewardTo");
            sp.Append(",@DepositReturnedRewardFrom");
            sp.Append(",@DepositReturnedRewardTo");
            sp.Append(",@DepositOverflowRewardFrom");
            sp.Append(",@DepositOverflowRewardTo");
            sp.Append(",@ExtraRewardFrom");
            sp.Append(",@ExtraRewardTo");
            sp.Append(",@TotalRewardFrom");
            sp.Append(",@TotalRewardTo");
            //sp.Append(",@SelfScorePctFrom");
            //sp.Append(",@SelfScorePctTo");
            //sp.Append(",@SelfScoreFrom");
            //sp.Append(",@SelfScoreTo");
            //sp.Append(",@TeamScorePctFrom");
            //sp.Append(",@TeamScorePctTo");
            //sp.Append(",@TeamScoreFrom");
            //sp.Append(",@TeamScoreTo");
            sp.Append(",@pageNumber");
            sp.Append(",@pageSize");
            sp.Append(",@outReturnCode OUT");
            sp.Append(",@outReturnMessage OUT");
            sp.Append(",@outTotalRewardEarned OUT");
            sp.Append(",@outTotalRecords OUT");
            sp.Append(",@outTotalPages OUT");
            #endregion Prepare Stored Procedure

            #region Execute Stored Procedure
            var returnList = _dbContext.SqlQuery<DepositRewardHistoryCustom>(
                sp.ToString()
                , new SqlParameter("@Username", Username ?? (object)DBNull.Value)
                , new SqlParameter("@DateFrom", DateFrom)
                , new SqlParameter("@DateTo", DateTo)
                //, new SqlParameter("@ContributedBy", ContributedBy ?? (object)DBNull.Value) //Jerry 20181009 MSP-209
                , new SqlParameter("@DepositAmountFrom", DepositAmountFrom ?? (object)DBNull.Value)
                , new SqlParameter("@DepositAmountTo", DepositAmountTo ?? (object)DBNull.Value)
                , new SqlParameter("@DepositReturnedFrom", DepositReturnedFrom ?? (object)DBNull.Value)
                , new SqlParameter("@DepositReturnedTo", DepositReturnedTo ?? (object)DBNull.Value)
                //, new SqlParameter("@DepositRewardFrom", DepositRewardFrom ?? (object)DBNull.Value)                       //20190221 MDT-257
                //, new SqlParameter("@DepositRewardTo", DepositRewardTo ?? (object)DBNull.Value)                           //20190221 MDT-257
                , new SqlParameter("@DepositReturnedRewardFrom", DepositReturnedRewardFrom ?? (object)DBNull.Value)         //20190221 MDT-257
                , new SqlParameter("@DepositReturnedRewardTo", DepositReturnedRewardTo ?? (object)DBNull.Value)             //20190221 MDT-257
                , new SqlParameter("@DepositOverflowRewardFrom", DepositOverflowRewardFrom ?? (object)DBNull.Value)         //20190221 MDT-257
                , new SqlParameter("@DepositOverflowRewardTo", DepositOverflowRewardTo ?? (object)DBNull.Value)           //20190221 MDT-257
                , new SqlParameter("@ExtraRewardFrom", ExtraRewardFrom ?? (object)DBNull.Value)
                , new SqlParameter("@ExtraRewardTo", ExtraRewardto ?? (object)DBNull.Value)
                , new SqlParameter("@TotalRewardFrom", TotalRewardFrom ?? (object)DBNull.Value)
                , new SqlParameter("@TotalRewardTo", TotalRewardTo ?? (object)DBNull.Value)
                //, new SqlParameter("@SelfScorePctFrom", SelfScorePctFrom ?? (object)DBNull.Value)
                //, new SqlParameter("@SelfScorePctTo", SelfScorePctTo ?? (object)DBNull.Value)
                //, new SqlParameter("@SelfScoreFrom", SelfScoreFrom ?? (object)DBNull.Value)
                //, new SqlParameter("@SelfScoreTo", SelfScoreTo ?? (object)DBNull.Value)
                //, new SqlParameter("@TeamScorePctFrom", TeamScorePctFrom ?? (object)DBNull.Value)
                //, new SqlParameter("@TeamScorePctTo", TeamScorePctTo ?? (object)DBNull.Value)
                //, new SqlParameter("@TeamScoreFrom", TeamScoreFrom ?? (object)DBNull.Value)
                //, new SqlParameter("@TeamScoreTo", TeamScoreTo ?? (object)DBNull.Value)
                , new SqlParameter("@pageNumber", pageNumber)
                , new SqlParameter("@pageSize", pageSize)
                , outReturnCode
                , outReturnMessage
                , outTotalRewardEarned
                , outTotalRecords
                , outTotalPages
                ).ToList();
            #endregion

            if (returnList.Count > 0)
            {
                TotalPage = Convert.ToInt32(outTotalPages.SqlValue.ToString());
                TotalRecord = Convert.ToInt32(outTotalRecords.SqlValue.ToString());
            }

            return returnList;
        }
        #endregion

        #endregion
    }
}

