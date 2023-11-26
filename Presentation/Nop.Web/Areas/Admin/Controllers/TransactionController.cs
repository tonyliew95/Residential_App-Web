using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Msp;
using Nop.Core.Domain.Msp.Custom;
using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Services.Customers;
using Nop.Services.ExportImport;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Transaction;
using Nop.Web.Areas.Admin.Models.Transaction;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class TransactionController : BaseAdminController
    {
        private readonly ITransactionService _transactionService;
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IMspHelper _mspHelper;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<MSP_Wallet> _walletRepository;
        private readonly IRepository<MSP_MemberTree> _membertreeRepository;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IExportManager _exportManager; //Jerry 20181218 MDT-158

        public TransactionController
            (
            ITransactionService transactionService,
            IPermissionService permissionService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IMspHelper mspHelper,
            ICustomerService customerService,
            IRepository<MSP_Wallet> walletService,
            IRepository<MSP_MemberTree> membertreeService,
            IRepository<Customer> repocustomerService,
            IDateTimeHelper dateTimeHelper
            , IExportManager exportManager //Jerry 20181218 MDT-158
            )
        {
            this._transactionService = transactionService;
            this._permissionService = permissionService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._mspHelper = mspHelper;
            this._customerService = customerService;
            this._walletRepository = walletService;
            this._membertreeRepository = membertreeService;
            this._customerRepository = repocustomerService;
            this._dateTimeHelper = dateTimeHelper;
            this._exportManager = exportManager; //Jerry 20181218 MDT-158
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        #region Transaction Listing 

        #region MSP-46 Backend Function: Transaction Listing > Top Up RW 20180823
        [HttpGet]
        public virtual IActionResult TopUpTxnList()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageTopUp))
                return AccessDeniedView();

            TxnTopUpListViewModel model = new TxnTopUpListViewModel();
            model.Status = _transactionService.GetTransactionStatusValue();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult TopUpTxnPagedList(DataSourceRequest command, TxnTopUpListViewModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageTopUp))
                return AccessDeniedKendoGridJson();
            //var model = new TxnTopUpListViewModel();

            var gridModel = new DataSourceResult();

            if (!model.DateFrom.HasValue || !model.DateTo.HasValue)
            {
                return Json(gridModel);
            }

            var dtTo = _mspHelper.ToDate(model.DateTo.Value);

            var list = _transactionService.GetTopUpDetails(
                Username: model.Username
                , DateFrom: _dateTimeHelper.ConvertToUtcTime(model.DateFrom.Value)
                , DateTo: _dateTimeHelper.ConvertToUtcTime(dtTo)
                , TxId: model.TxnId
                , TopUpAmt: model.TopUpAmt
                , TopUpAmtMBTCAdd: model.TopUpMbtcAdd
                //wailiang 20190116 MDT-193 \/
                , BlockchainTxId: model.BlockchainTxId
                , Status: model.StatusValue
                //wailiang 20190116 MDT-193 /\
                , pageIndex: command.Page - 1
                , pageSize: command.PageSize
                );

            //var pagedlist = new PagedList<MSP_Mbtc_Deposit>(list, pageIndex: command.Page - 1, pageSize: command.PageSize);

            //var TopUplist = list.PagedForCommand(command).Select(o => new TxnTopUpListViewModel.TxnTopUp
            // {
            //     Username = _customerService.GetCustomerUsernameById(o.CustomerID),
            //     Date = o.CreatedOnUtc,
            //     TxId = o.TxID,
            //     TopUpAmt = o.MbtcAmt,
            //     TopUpWalletAdd = o.WalletAddress
            // }
            // );

            gridModel = new DataSourceResult
            {
                Data = list.Select(o => new TxnTopUpListViewModel.TxnTopUp
                {
                    Username = _customerService.GetCustomerUsernameById(o.CustomerID),
                    Date = o.CreatedOnUtc,
                    TxId = o.TxID,
                    TopUpAmt = o.MbtcAmt,
                    TopUpWalletAdd = o.WalletAddress,
                    //wailiang 20190116 MDT-193 \/
                    BlockchainTxId = o.BlockChainTxId,
                    Status = o.StatusDescription
                    //wailiang 20190116 MDT-193 /\
                }),
                Total = list.TotalCount
            };

            return Json(gridModel);
        }

        // 20181217 WKK MDT-153 MSP requirement 1.2 > Export function to CSV > Transaction Listing → Top Up
        [HttpPost, ActionName("TopUpTxnList")]
        [FormValueRequired("exportcsvalltopup")]
        public virtual IActionResult ExportCsvAllTopup(TxnTopUpListViewModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageTopUp))
                return AccessDeniedView();

            var dtTo = _mspHelper.ToDate(model.DateTo.Value);

            var list = _transactionService.GetTopUpDetails(
                Username: model.Username
                , DateFrom: _dateTimeHelper.ConvertToUtcTime(model.DateFrom.Value)
                , DateTo: _dateTimeHelper.ConvertToUtcTime(dtTo)
                , TxId: model.TxnId
                , TopUpAmt: model.TopUpAmt
                , TopUpAmtMBTCAdd: model.TopUpMbtcAdd
                //wailiang 20190116 MDT-193 \/
                , BlockchainTxId: model.BlockchainTxId
                , Status: model.StatusValue
                //wailiang 20190116 MDT-193 /\
                , pageIndex: 0
                , pageSize: 99999
                );

            var Data = list.Select(o => new TxnTopUpListViewModel.TxnTopUp
            {
                Username = _customerService.GetCustomerUsernameById(o.CustomerID),
                Date = o.CreatedOnUtc,
                TxId = o.TxID,
                TopUpAmt = o.MbtcAmt,
                TopUpWalletAdd = o.WalletAddress,
                //wailiang 20190116 MDT-193 \/
                Status = o.StatusDescription,
                BlockchainTxId = o.BlockChainTxId
                //wailiang 20190116 MDT-193 /\
            });

            const string separator = ",";
            var sb = new StringBuilder();

            // Column name
            sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.Username"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.Date"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.TxnId"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.TopUpAmt"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.TopUpMbtcAdd"));
            sb.Append(separator);
            //wailiang 20190116 MDT-193 \/
            sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.Status"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.BlockchainTxId"));
            //wailiang 20190116 MDT-193 /\
            sb.Append(Environment.NewLine); //new line

            // Column Detail
            foreach (var item in Data)
            {
                sb.Append(item.Username);
                sb.Append(separator);
                sb.Append(item.Date);
                sb.Append(separator);
                sb.Append(item.TxId);
                sb.Append(separator);
                sb.Append(item.TopUpAmt);
                sb.Append(separator);
                sb.Append(item.TopUpWalletAdd);
                //wailiang 20190116 MDT-193 \/
                sb.Append(separator);
                sb.Append(item.Status);
                sb.Append(separator);
                sb.Append(item.BlockchainTxId);
                //wailiang 20190116 MDT-193 /\
                sb.Append(Environment.NewLine); //new line
            }

            var fileName = $"transaction_topup.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), MimeTypes.TextCsv, fileName);
        }

        #region Comment Code
        // 20181217 WKK MDT-153 MSP requirement 1.2 > Export function to CSV > Transaction Listing → Top Up
        //public virtual IActionResult ExportCsvSelectedTopup(string selectedIds)
        //{
        //	if (!_permissionService.Authorize(StandardPermissionProvider.ManageTopUp))
        //		return AccessDeniedView();

        //	var trans = new List<MSP_Mbtc_Deposit>();
        //	if (selectedIds != null)
        //	{
        //		var ids = selectedIds
        //			.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        //			.Select(x => Convert.ToInt32(x))
        //			.ToArray<int>();
        //		trans.AddRange(_transactionService.GetTopupByIds(ids));
        //	}

        //	var Data = trans.Select(o => new TxnTopUpListViewModel.TxnTopUp
        //	{
        //		Username = _customerService.GetCustomerUsernameById(o.CustomerID),
        //		Date = o.CreatedOnUtc,
        //		TxId = o.TxID,
        //		TopUpAmt = o.MbtcAmt,
        //		TopUpWalletAdd = o.WalletAddress
        //	});

        //	const string separator = ",";
        //	var sb = new StringBuilder();

        //	// Column name
        //	sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.Username"));
        //	sb.Append(separator);
        //	sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.Date"));
        //	sb.Append(separator);
        //	sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.TxnId"));
        //	sb.Append(separator);
        //	sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.TopUpAmt"));
        //	sb.Append(separator);
        //	sb.Append(_localizationService.GetResource("Admin.TransactionList.TopUp.List.Fields.TopUpMbtcAdd"));
        //	sb.Append(Environment.NewLine); //new line

        //	// Column Detail
        //	foreach (var item in Data)
        //	{
        //		sb.Append(item.Username);
        //		sb.Append(separator);
        //		sb.Append(item.Date);
        //		sb.Append(separator);
        //		sb.Append(item.TxId);
        //		sb.Append(separator);
        //		sb.Append(item.TopUpAmt);
        //		sb.Append(separator);
        //		sb.Append(item.TopUpWalletAdd);
        //		sb.Append(Environment.NewLine); //new line
        //	}

        //	var fileName = $"transaction_topup.csv";
        //	return File(Encoding.UTF8.GetBytes(sb.ToString()), MimeTypes.TextCsv, fileName);
        //}
        #endregion

        #endregion

        #region Deposit 
        public virtual IActionResult TxnDepositList(int id) //Atiqah 20190903 MSP-94
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDeposit))
                return AccessDeniedView();

            var model = new DepositListModel();
            model.Status = _transactionService.GetTransactionStatusValue();
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult TxnDepositList(DataSourceRequest command, DepositListModel model) //Atiqah 20190903 MSP-94
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDeposit))
                return AccessDeniedKendoGridJson();

            var gridModel = new DataSourceResult();

            if (!model.DateFrom.HasValue || !model.DateTo.HasValue)
            {
                return Json(gridModel);
            }

            #region Comment Code
            //Atiqah 20180925 MSP-94 \/
            //var depositLists = _transactionService.GetDepositDetails(model.Username
            //    , _dateTimeHelper.ConvertToUtcTime(model.DateFrom.Value), _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(model.DateTo.Value))
            //    , model.TxnNo
            //    , model.DepositAmt);

            //gridModel = new DataSourceResult
            //{
            //    Data = depositLists.PagedForCommand(command).Select(x =>
            //    {
            //        var _model = new DepositModel
            //        {
            //            Username = _customerService.GetCustomerUsernameById(x.CustomerID),
            //            Date = x.CreatedOnUtc,
            //            TxnNo = x.Id,
            //            DepositAmt = x.DepositAmt
            //        };

            //        return _model;
            //    }),
            //    Total = depositLists.Count
            //};
            //Atiqah 20180925 MSP-94 /\
            #endregion

            //Atiqah 20180925 MSP-94 \/
            var depositLists = _transactionService.GetDepositDetails(
                username: model.Username,
                DateFrom: _dateTimeHelper.ConvertToUtcTime(model.DateFrom.Value),
                DateTo: _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(model.DateTo.Value)),
                TxnNo: model.TxnNo,
                DepositAmt: model.DepositAmt,
                status: model.StatusValue,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);
            //Atiqah 20180925 MSP-94 /\

            gridModel = new DataSourceResult
            {
                Data = depositLists.Select(x =>
                {
                    var _model = new DepositModel
                    {
                        Username = x.Username,
                        Date = x.Date,
                        TxnNo = x.TxnNo,
                        DepositAmt = x.DepositAmt,
                        //wailiang 20190115 MDT-194 \/
                        Status = x.Status
                        //wailiang 20190115 MDT-194 /\
                    };

                    return _model;
                }),
                Total = depositLists.TotalCount
            };
            return Json(gridModel);
        }

        // 20181218 WKK MDT-154	MSP requirement 1.2 > Export function to CSV > Transaction Listing → Agency Fee
        [HttpPost, ActionName("TxnDepositList")]
        [FormValueRequired("exportcsvallagencyfee")]
        public virtual IActionResult ExportCsvAllAgencyFee(DepositListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDeposit))
                return AccessDeniedView();

            var depositLists = _transactionService.GetDepositDetails(
                username: model.Username,
                DateFrom: _dateTimeHelper.ConvertToUtcTime(model.DateFrom.Value),
                DateTo: _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(model.DateTo.Value)),
                TxnNo: model.TxnNo,
                DepositAmt: model.DepositAmt,
                status: model.StatusValue,
                pageIndex: 0,
                pageSize: 99999);

            var Data = depositLists.Select(x =>
            {
                var _model = new DepositModel
                {
                    Username = x.Username,
                    Date = x.Date,
                    TxnNo = x.TxnNo,
                    DepositAmt = x.DepositAmt,
                    //wailiang 20190115 MDT-194 \/
                    Status = x.Status,
                    //wailiang 20190115 MDT-194 /\
                };

                return _model;
            });

            const string separator = ",";
            var sb = new StringBuilder();

            // Column name
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Deposit.Fields.Username"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Deposit.Fields.Date"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Deposit.Fields.TransactionNo"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Deposit.Fields.DepositAmount"));
            sb.Append(separator);
            //wailiang 20190115 MDT-194 \/
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Deposit.Fields.Status"));
            sb.Append(separator);
            //sb.Append(_localizationService.GetResource("Admin.TransactionList.Deposit.Fields.Remark"));//Tony Liew 20190124 MSP-697
            //sb.Append(separator);//Tony Liew 20190124 MSP-697
            //wailiang 20190115 MDT-194 /\
            sb.Append(Environment.NewLine); //new line

            // Column Detail
            foreach (var item in Data)
            {
                sb.Append(item.Username);
                sb.Append(separator);
                sb.Append(item.Date);
                sb.Append(separator);
                sb.Append(item.TxnNo);
                sb.Append(separator);
                sb.Append(item.DepositAmt);
                //wailiang 20190115 MDT-194 \/
                sb.Append(separator); //Tony Liew 20190124 MSP-697
                sb.Append(item.Status);
                //sb.Append(item.Remark);//Tony Liew 20190124 MSP-697
                //wailiang 20190115 MDT-194 /\
                sb.Append(Environment.NewLine); //new line
            }

            var fileName = $"transaction_agencyfee.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), MimeTypes.TextCsv, fileName);
        }

        #endregion

        #region Withdrawal
        //wailiang 20190910 MSP-95 \/
        public virtual IActionResult WithdrawalList(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWithdrawal))
                return AccessDeniedView();

            var model = new WithdrawalListModel();
            model.Status = _transactionService.GetWithdrawalStatusValue();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult WithdrawalList(DataSourceRequest command, WithdrawalListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWithdrawal))
                return AccessDeniedKendoGridJson();

            //wailiang 20180912 MSP-95 \/
            if (!model.DateFrom.HasValue || !model.DateTo.HasValue)
            {
                return Json(model);
            }
            //wailiang 20180912 MSP-95 /\

            #region comment code
            //int _txnNo = 0;
            //decimal _amt = 0.0m;
            //decimal _txnFees = 0.0m;
            //decimal _netAmt = 0.0m;
            //if (!string.IsNullOrEmpty(model.TxnNo))
            //    if(!int.TryParse(model.TxnNo, out _txnNo))
            //        _txnNo = 0;

            //if (!string.IsNullOrEmpty(model.Amount))
            //    if (!decimal.TryParse(model.Amount, out _amt))
            //        _amt = 0.0m;

            //if (!string.IsNullOrEmpty(model.TransactionFees))
            //    if(!decimal.TryParse(model.TransactionFees, out _txnFees))
            //        _txnFees = 0.0m;

            //if (!string.IsNullOrEmpty(model.NetAmount))
            //    if(!decimal.TryParse(model.NetAmount, out _netAmt))
            //        _netAmt = 0.0m;

            //var withdrawalLists = _transactionService.GetAllWithdrawal(model.Username, model.TxnNo, model.Amount, model.TransactionFees, model.NetAmount, model.DateFrom, model.DateTo);

            //var gridModel = new DataSourceResult
            //{
            //    Data = withdrawalLists.PagedForCommand(command).Select(x =>
            //    {
            //        var _model = new WithdrawalModel
            //        {
            //            Username = _customerService.GetCustomerUsernameById(x.CustomerID),
            //            Date = x.CreatedOnUtc,
            //            TxnNo = x.Id,
            //            Amount = _mspHelper.TruncateDecimalToString_MBTC(x.WithdrawAmt),
            //            TransactionFees = _mspHelper.TruncateDecimalToString_MBTC(x.TxFee),
            //            NetAmount = _mspHelper.TruncateDecimalToString_MBTC(x.NetWithdrawAmt)
            //        };

            //        return _model;
            //    }),
            //    Total = withdrawalLists.Count
            //};

            //return Json(gridModel);
            //wailiang 20180912 MSP-95 /\
            #endregion

            var withdrawalLists = _transactionService.GetAllWithdrawal(
                username: model.Username,
                DateFrom: model.DateFrom,
                DateTo: model.DateTo,
                _txnNo: model.TxnNo,
                _amt: model.Amount,
                _txnFees: model.TransactionFees,
                _netAmt: model.NetAmount,
                _withdrawalAddress: model.WithdrawalAddress,
                _blockchainTxId: model.BlockChainTxId,
                _status: model.StatusValue,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize
                );

            var gridModel = new DataSourceResult
            {
                Data = withdrawalLists.Select(x =>
                {
                    var _model = new WithdrawalModel
                    {
                        Username = x.Username,
                        Date = x.Date,
                        TxnNo = x.TxnNo,
                        Amount = _mspHelper.TruncateDecimalToString_MBTC(x.Amount),
                        TransactionFees = _mspHelper.TruncateDecimalToString_MBTC(x.TransactionFees),
                        NetAmount = _mspHelper.TruncateDecimalToString_MBTC(x.NetAmount),
                        //wailiang 20190116 MDT-195 \/
                        WithdrawalAddress = x.WithdrawalAddress,
                        BlockChainTxId = x.BlockChainTxId,
                        Status = x.Status,
                        RefundStatus = x.RefundStatus,
                        Remark = x.Remark
                        //wailiang 20190116 MDT-195 /\
                    };

                    return _model;
                }),
                Total = withdrawalLists.TotalCount
            };

            return Json(gridModel);
        }
        //wailiang 20190910 MSP-95 /\

        // 20181219 WKK MDT-155	MSP requirement 1.2 > Export function to CSV > Transaction Listing → Withdrawal
        [HttpPost, ActionName("WithdrawalList")]
        [FormValueRequired("exportcsvallwithdrawal")]
        public virtual IActionResult ExportCsvAllWithdrawal(WithdrawalListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWithdrawal))
                return AccessDeniedView();

            var withdrawalLists = _transactionService.GetAllWithdrawal(
                username: model.Username,
                DateFrom: model.DateFrom,
                DateTo: model.DateTo,
                _txnNo: model.TxnNo,
                _amt: model.Amount,
                _txnFees: model.TransactionFees,
                _netAmt: model.NetAmount,
                _withdrawalAddress: model.WithdrawalAddress,
                _blockchainTxId: model.BlockChainTxId,
                _status: model.StatusValue,
                pageIndex: 0,
                pageSize: 99999);

            var Data = withdrawalLists.Select(x =>
            {
                var _model = new WithdrawalModel
                {
                    Username = x.Username,
                    Date = x.Date,
                    TxnNo = x.TxnNo,
                    Amount = _mspHelper.TruncateDecimalToString_MBTC(x.Amount),
                    TransactionFees = _mspHelper.TruncateDecimalToString_MBTC(x.TransactionFees),
                    NetAmount = _mspHelper.TruncateDecimalToString_MBTC(x.NetAmount),
                    //wailiang 20190116 MDT-195 \/
                    WithdrawalAddress = x.WithdrawalAddress,
                    BlockChainTxId = x.BlockChainTxId,
                    Status = x.Status,
                    RefundStatus = x.RefundStatus,
                    Remark = x.Remark,
                    //wailiang 20190116 MDT-195 /\
                };

                return _model;
            });

            const string separator = ",";
            var sb = new StringBuilder();

            // Column name
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.Username"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.Date"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.TxnId"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.Amount.mbtc"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.TransactionFees.mbtc"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.NetAmount.mbtc"));
            //wailiang 20190116 MDT-195 \/
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.WithdrawalAddress"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.BlockchainTxId"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.Status"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.RefundStatus"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.Withdrawal.Fields.Remark"));
            //wailiang 20190116 MDT-195 /\
            sb.Append(Environment.NewLine); //new line

            // Column Detail
            foreach (var item in Data)
            {
                sb.Append(item.Username);
                sb.Append(separator);
                sb.Append(item.Date);
                sb.Append(separator);
                sb.Append(item.TxnNo);
                sb.Append(separator);
                sb.Append(item.Amount);
                sb.Append(separator);
                sb.Append(item.TransactionFees);
                sb.Append(separator);
                sb.Append(item.NetAmount);
                sb.Append(separator);
                //wailiang 20190116 MDT-195 \/
                sb.Append(item.WithdrawalAddress);
                sb.Append(separator);
                sb.Append(item.BlockChainTxId);
                sb.Append(separator);
                sb.Append(item.Status);
                sb.Append(separator);
                sb.Append(item.RefundStatus);
                sb.Append(separator);
                sb.Append(item.Remark);
                //wailiang 20190116 MDT-195 /\
                sb.Append(Environment.NewLine); //new line
            }

            var fileName = $"transaction_withdrawal.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), MimeTypes.TextCsv, fileName);
        }

        #endregion

        #region Consumption Reward History
        //public virtual IActionResult ConsumptionRewardHistoryList(string username, string date) //wailiang 20180921 MSP-98 //Jerry 20181102 MSP-436
        public virtual IActionResult ConsumptionRewardHistoryList(
            string username
            , string date
            , DateTime xFromDate
            , DateTime xToDate
            , string xUsername
            , int xPlatformID
            , string xDepositReturnFrom
            , string xDepositReturnTo
            , string xMemberRewardFrom
            , string xMemberRewardTo
            , string xConsumptionFrom
            , string xConsumptionTo
            //, string xMerchantRefFrom //Atiqah 20190219 MDT-244 
            //, string xMerchantRefTo //Atiqah 20190219 MDT-244 
            , string xTotalRewardFrom
            , string xTotalRewardTo
            ) //Jerry 20181102 MSP-436
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardHistory))
                return AccessDeniedView();

            if (username != null && date != null)
            {
                ViewBag.Username = username;
                ViewBag.Datetime = date;
            }

            //Jerry 20181102 MSP-436 \/
            //var model = new ConsumptionRewardModel();
            var model = new ConsumptionRewardCombineModel();
            model.SelfListModel.IsRefreshData = true;
            model.SelfListModel.FromDate = xFromDate;
            model.SelfListModel.ToDate = xToDate;
            model.SelfListModel.Username = xUsername;
            model.SelfListModel.PlatformID = xPlatformID;
            model.SelfListModel.DepositReturnFrom = xDepositReturnFrom;
            model.SelfListModel.DepositReturnTo = xDepositReturnTo;
            model.SelfListModel.MemberRewardFrom = xMemberRewardFrom;
            model.SelfListModel.MemberRewardTo = xMemberRewardTo;
            model.SelfListModel.ConsumptionFrom = xConsumptionFrom;
            model.SelfListModel.ConsumptionTo = xConsumptionTo;
            //model.SelfListModel.MerchantRefFrom = xMerchantRefFrom;  //Atiqah 20190219 MDT-244
            //model.SelfListModel.MerchantRefTo = xMerchantRefTo;  //Atiqah 20190219 MDT-244
            model.SelfListModel.TotalRewardFrom = xTotalRewardFrom;
            model.SelfListModel.TotalRewardTo = xTotalRewardTo;
            //Jerry 20181102 MSP-436 /\

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ConsumptionRewardHistoryList(DataSourceRequest command, string username, string date) //wailiang 20180921 MSP-98
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardHistory))
                return AccessDeniedKendoGridJson();

            int customerId = _customerService.GetCustomerByUsername(username).Id;
            DateTime datetime = DateTime.ParseExact(date, "yyyy-MM-dd", null);

            var consumptionLists = _transactionService.ConsumptionRewardHistoryList(
                customerId: customerId,
                date: datetime,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = consumptionLists.Select(x =>
                {
                    var _model = new ConsumptionRewardModel
                    {
                        Date = x.Date.ToString(),
                        PlatformName = x.PlatformName.ToString(),
                        TotalDepositReturned = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.DepositReturned)),
                        TotalMembershipReward = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.MembershipReward)),
                        TotalConsumptionReward = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.ConsumptionReward)),
                        TotalHonoraryCitizenReward = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.HonoraryCitizenReward)),
                        GrandTotalReward = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.TotalReward))
                    };

                    return _model;
                }),
                Total = consumptionLists.TotalCount
            };

            return Json(gridModel);
        }
        #endregion

        #region Redeem Transaction List
        public virtual IActionResult RedeemTransactionsList() //JK 20180912 MSP-96
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageRedeem))
                return AccessDeniedView();

            var model = new RedeemTransactionsListModel();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult RedeemTransactionsList(DataSourceRequest command, RedeemTransactionsListModel model) //JK 20180912 MSP-96
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageRedeem))
                return AccessDeniedKendoGridJson();

            var list = _transactionService.GetAllRedeemTransactions(model.Username, model.WalletID, model.AvailableBalanceFrom, model.AvailableBalanceTo, model.RedeemWalletBalanceFrom, model.RedeemWalletBalanceTo, command.Page - 1, command.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = list.Select(x =>
                {
                    var _model = new RedeemTransactionsModel
                    {
                        Username = x.Username,
                        AvailableBalance = _mspHelper.TruncateDecimalToString_MBTC(x.AvailableBalance),
                        RedeemWalletBalance = _mspHelper.TruncateDecimalToString_MBTC(x.RedeemWalletBalance)
                    };

                    return _model;
                }),
                Total = list.TotalCount
            };

            return Json(gridModel);
        }

        // 20181219 WKK MDT-156			MDT-136 MSP requirement 1.2 > Export function to CSV > Transaction Listing → Locked Earning
        [HttpPost, ActionName("RedeemTransactionsList")]
        [FormValueRequired("exportcsvallredeem")]
        public virtual IActionResult ExportCsvAllRedeem(RedeemTransactionsListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageRedeem))
                return AccessDeniedView();

            var list = _transactionService.GetAllRedeemTransactions(model.Username, model.WalletID, model.AvailableBalanceFrom, model.AvailableBalanceTo, model.RedeemWalletBalanceFrom, model.RedeemWalletBalanceTo, 0, 99999);

            var Data = list.Select(x =>
            {
                var _model = new RedeemTransactionsModel
                {
                    Username = x.Username,
                    AvailableBalance = _mspHelper.TruncateDecimalToString_MBTC(x.AvailableBalance),
                    RedeemWalletBalance = _mspHelper.TruncateDecimalToString_MBTC(x.RedeemWalletBalance)
                };

                return _model;
            });

            const string separator = ",";
            var sb = new StringBuilder();

            // Column name
            sb.Append(_localizationService.GetResource("Admin.TransactionList.RedeemTransactions.Fields.Username"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.RedeemTransactions.Fields.AvailableBalance"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.RedeemTransactions.Fields.RedeemWalletBalance"));
            sb.Append(Environment.NewLine); //new line

            // Column Detail
            foreach (var item in Data)
            {
                sb.Append(item.Username);
                sb.Append(separator);
                sb.Append(item.AvailableBalance);
                sb.Append(separator);
                sb.Append(item.RedeemWalletBalance);
                sb.Append(Environment.NewLine); //new line
            }

            var fileName = $"transaction_redeem.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), MimeTypes.TextCsv, fileName);
        }

        #endregion

        #region Consumption Reward Self
        //public virtual IActionResult ConsumptionRewardSelfList() //JK 20180912 MSP-96 //Jerry 20181102 MSP-436
        public virtual IActionResult ConsumptionRewardSelfList(ConsumptionRewardSelfListModel model) //Jerry 20181102 MSP-436
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedView();

            //Jerry 20181102 MSP-436 \/
            //var model = new ConsumptionRewardSelfListModel(); 
            model.FromDate = (model.FromDate == new DateTime() ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : model.FromDate);
            model.ToDate = (model.ToDate == new DateTime() ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) : model.ToDate);
            //Jerry 20181102 MSP-436 /\
            model.PlatformNameOptions = _transactionService.GetPlatformNameValue();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ConsumptionRewardSelfList(DataSourceRequest command, ConsumptionRewardSelfListModel model) //JK 20180912 MSP-96
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedKendoGridJson();

            //wailiang 20181003 MSP-96 \/
            //var list = _transactionService.GetConsumptionRewardSelfList(model.FromDate, model.ToDate, model.Username, model.PlatformID,
            //    model.DepositReturnFrom, model.DepositReturnTo, model.MemberRewardFrom, model.MemberRewardTo, model.ConsumptionFrom, model.ConsumptionTo,
            //    model.MerchantRefFrom, model.MerchantRefTo, model.TotalRewardFrom, model.TotalRewardTo, command.Page - 1, command.PageSize);

            var list = _transactionService.GetConsumptionRewardSelfList(
                    model.FromDate,
                    model.ToDate,
                    model.Username,
                    model.PlatformID,
                    model.DepositReturnFrom,
                    model.DepositReturnTo,
                    model.MemberRewardFrom,
                    model.MemberRewardTo,
                    model.ConsumptionFrom,
                    model.ConsumptionTo,
                    //model.MerchantRefFrom, //Atiqah 20190219 MDT-244
                    //model.MerchantRefTo, //Atiqah 20190219 MDT-244
                    model.TotalRewardFrom,
                    model.TotalRewardTo);

            //Chew 20190125 MSP-695 \/
            var data = new PagedList<ConsumptionRewardSelfCustom>(list, pageIndex: command.Page - 1, pageSize: command.PageSize);
            //Chew 20190125 MSP-695 /\
            var gridModel = new DataSourceResult
            {
                Data = data.Select(x => new ConsumptionRewardSelfModel
                {
                    Username = x.Username,
                    Date = x.Date,
                    PlatformName = x.PlatformName,
                    #region Comment Code
                    //Atiqah 20181023 MSP-364 \/
                    //Deposit_Returned = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.DepositReturned)),
                    //Membership_Reward = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.MembershipReward)),
                    //Consumption_Reward = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.ConsumptionReward)),
                    //Merchant_Ref_Reward = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.MerchantReferralReward)),
                    //Total_Reward = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(x.TotalReward))
                    //Atiqah 20181023 MSP-364 /\
                    #endregion Comment Code
                    //Atiqah 20181023 MSP-364 \/
                    Deposit_Returned = _mspHelper.TruncateDecimalToString_MBTC(x.DepositReturned),
                    Membership_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.MembershipReward),
                    Consumption_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.ConsumptionReward),
                    //Merchant_Ref_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.MerchantReferralReward), //Atiqah 20190219 MDT-244
                    Total_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.TotalReward)
                    //Atiqah 20181023 MSP-364 /\
                }),
                Total = data.TotalCount
            };
            return Json(gridModel);
            //wailiang 20181003 MSP-96 /\
        }

        // 20181220 WKK MDT-157		MSP requirement 1.2 > Export function to CSV > Transaction Listing → Task Reward Listing	
        [HttpPost, ActionName("ConsumptionRewardSelfList")]
        [FormValueRequired("exportcsvalltaskreward")]
        public virtual IActionResult ExportCsvAllTaskreward(ConsumptionRewardSelfListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedView();

            var list = _transactionService.GetConsumptionRewardSelfList(
                    model.FromDate, model.ToDate,
                    model.Username, model.PlatformID,
                    model.DepositReturnFrom, model.DepositReturnTo,
                    model.MemberRewardFrom, model.MemberRewardTo,
                    model.ConsumptionFrom, model.ConsumptionTo,
                    //model.MerchantRefFrom,	model.MerchantRefTo, //Atiqah 20190219 MDT-244
                    model.TotalRewardFrom, model.TotalRewardTo);

            var Data = list.Select(x =>
            {
                var _model = new ConsumptionRewardSelfModel
                {
                    Username = x.Username,
                    Date = x.Date,
                    PlatformName = x.PlatformName,
                    Deposit_Returned = _mspHelper.TruncateDecimalToString_MBTC(x.DepositReturned),
                    Membership_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.MembershipReward),
                    Consumption_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.ConsumptionReward),
                    //Merchant_Ref_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.MerchantReferralReward), //Atiqah 20190219 MDT-244
                    Total_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.TotalReward)
                };
                return _model;
            });

            const string separator = ",";
            var sb = new StringBuilder();

            // Column name
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.Username"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.Date"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.PlatformName"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.DepositReturned"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.MembershipReward"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.ConsumptionReward"));
            sb.Append(separator);
            //sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.MerchantRefReward")); //Atiqah 20190219 MDT-244
            //sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.TotalReward"));
            sb.Append(Environment.NewLine); //new line

            // Column Detail
            foreach (var item in Data)
            {
                sb.Append(item.Username);
                sb.Append(separator);
                sb.Append(item.Date);
                sb.Append(separator);
                sb.Append(item.PlatformName);
                sb.Append(separator);
                sb.Append(item.Deposit_Returned);
                sb.Append(separator);
                sb.Append(item.Membership_Reward);
                sb.Append(separator);
                sb.Append(item.Consumption_Reward);
                sb.Append(separator);
                //sb.Append(item.Merchant_Ref_Reward); //Atiqah 20190219 MDT-244
                //sb.Append(separator);
                sb.Append(item.Total_Reward);
                sb.Append(Environment.NewLine); //new line
            }

            var fileName = $"Transaction_TaskReward.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), MimeTypes.TextCsv, fileName);
        }

        #endregion

        // WKK 20190220 MDT-248 Admin Panel > Team Task Reward
        #region Consumption Reward Team
        public virtual IActionResult ConsumptionRewardTeamList(ConsumptionRewardSelfListModel model) //Jerry 20181102 MSP-436
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedView();

            model.FromDate = (model.FromDate == new DateTime() ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : model.FromDate);
            model.ToDate = (model.ToDate == new DateTime() ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) : model.ToDate);
            model.PlatformNameOptions = _transactionService.GetPlatformNameValue();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ConsumptionRewardTeamList(DataSourceRequest command, ConsumptionRewardSelfListModel model) //JK 20180912 MSP-96
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedKendoGridJson();

            var list = _transactionService.GetConsumptionRewardTeamList(
                    model.FromDate,
                    model.ToDate,
                    model.Username,
                    model.PlatformID,
                    model.DepositReturnFrom,
                    model.DepositReturnTo,
                    model.MemberRewardFrom,
                    model.MemberRewardTo,
                    model.ConsumptionFrom,
                    model.ConsumptionTo,
                    model.HonoraryCitizenRewardFrom,
                    model.HonoraryCitizenRewardTo,
                    model.TotalRewardFrom,
                    model.TotalRewardTo);

            var data = new PagedList<ConsumptionRewardSelfCustom>(list, pageIndex: command.Page - 1, pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = data.Select(x => new ConsumptionRewardSelfModel
                {
                    Username = x.Username,
                    Date = x.Date,
                    PlatformName = x.PlatformName,
                    Deposit_Returned = _mspHelper.TruncateDecimalToString_MBTC(x.DepositReturned),
                    Membership_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.MembershipReward),
                    Consumption_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.ConsumptionReward),
                    TotalHonoraryCitizenReward = _mspHelper.TruncateDecimalToString_MBTC(x.HonoraryCitizenReward), 
                    Total_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.TotalReward)
                }),
                Total = data.TotalCount
            };
            return Json(gridModel);
        }

        [HttpPost, ActionName("ConsumptionRewardTeamList")]
        [FormValueRequired("exportcsv")]
        public virtual IActionResult ExportCsvTeam(ConsumptionRewardSelfListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedView();

            var list = _transactionService.GetConsumptionRewardTeamList(
                    model.FromDate, model.ToDate,
                    model.Username, model.PlatformID,
                    model.DepositReturnFrom, model.DepositReturnTo,
                    model.MemberRewardFrom, model.MemberRewardTo,
                    model.ConsumptionFrom, model.ConsumptionTo,
                    model.HonoraryCitizenRewardFrom,
                    model.HonoraryCitizenRewardTo,
                    model.TotalRewardFrom, model.TotalRewardTo);

            var Data = list.Select(x =>
            {
                var _model = new ConsumptionRewardSelfModel
                {
                    Username = x.Username,
                    Date = x.Date,
                    PlatformName = x.PlatformName,
                    Deposit_Returned = _mspHelper.TruncateDecimalToString_MBTC(x.DepositReturned),
                    Membership_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.MembershipReward),
                    Consumption_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.ConsumptionReward),
                    TotalHonoraryCitizenReward = _mspHelper.TruncateDecimalToString_MBTC(x.HonoraryCitizenReward),
                    Total_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.TotalReward)
                };
                return _model;
            });

            const string separator = ",";
            var sb = new StringBuilder();

            // Column name
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.Username"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.Date"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.PlatformName"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.DepositReturned"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.MembershipReward"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardHistory.Fields.TotalConsumptionReward.mbtc"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardHistory.Fields.TotalHonoraryCitizenReward.mbtc")); 
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.TotalReward"));
            sb.Append(Environment.NewLine); //new line

            // Column Detail
            foreach (var item in Data)
            {
                sb.Append(item.Username);
                sb.Append(separator);
                sb.Append(item.Date);
                sb.Append(separator);
                sb.Append(item.PlatformName);
                sb.Append(separator);
                sb.Append(item.Deposit_Returned);
                sb.Append(separator);
                sb.Append(item.Membership_Reward);
                sb.Append(separator);
                sb.Append(item.Consumption_Reward);
                sb.Append(separator);
                sb.Append(item.TotalHonoraryCitizenReward); 
                sb.Append(separator);
                sb.Append(item.Total_Reward);
                sb.Append(Environment.NewLine); //new line
            }

            var fileName = $"Transaction_TeamTaskReward.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), MimeTypes.TextCsv, fileName);
        }

        #endregion

        // WKK 20190219 MDT-246 Admin Panel > Merchant Referral Reward
        #region Consumption Reward Merchant Referral
        public virtual IActionResult ConsumptionRewardMerchantReferralList(ConsumptionRewardMerchantReferralListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedView();

            model.FromDate = (model.FromDate == new DateTime() ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) : model.FromDate);
            model.ToDate = (model.ToDate == new DateTime() ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) : model.ToDate);
            model.PlatformNameOptions = _transactionService.GetPlatformNameValue();

            return View(model);
        }

        //WKK 20190219 MDT-246 Admin Panel > Merchant Referral Reward
        [HttpPost]
        public virtual IActionResult ConsumptionRewardMerchantReferralList(DataSourceRequest command, ConsumptionRewardMerchantReferralListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedKendoGridJson();

            var list = _transactionService.GetConsumptionRewardMerchantReferralList(
                    model.FromDate,
                    model.ToDate,
                    model.Username,
                    model.PlatformID,
                    model.MerchantRefFrom,
                    model.MerchantRefTo);

            var data = new PagedList<ConsumptionRewardSelfCustom>(list, pageIndex: command.Page - 1, pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = data.Select(x => new ConsumptionRewardMerchantReferralModel
                {
                    Username = x.Username,
                    Date = x.Date,
                    PlatformName = x.PlatformName,
                    Merchant_Ref_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.MerchantReferralReward),
                }),
                Total = data.TotalCount
            };
            return Json(gridModel);
        }

        //WKK 20190219 MDT-246 Admin Panel > Merchant Referral Reward
        [HttpPost, ActionName("ConsumptionRewardMerchantReferralList")]
        [FormValueRequired("exportcsv")]
        public virtual IActionResult ExportCsvMerchantReferral(ConsumptionRewardMerchantReferralListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageConsumptionRewardSelf))
                return AccessDeniedView();

            var list = _transactionService.GetConsumptionRewardMerchantReferralList(
                    model.FromDate, model.ToDate,
                    model.Username, model.PlatformID,
                    model.MerchantRefFrom, model.MerchantRefTo);

            var Data = list.Select(x =>
            {
                var _model = new ConsumptionRewardMerchantReferralModel
                {
                    Username = x.Username,
                    Date = x.Date,
                    PlatformName = x.PlatformName,
                    Merchant_Ref_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.MerchantReferralReward)
                };
                return _model;
            });

            const string separator = ",";
            var sb = new StringBuilder();

            // Column name
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.Username"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.Date"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.PlatformName"));
            sb.Append(separator);
            sb.Append(_localizationService.GetResource("Admin.TransactionList.ConsumptionRewardSelf.Fields.MerchantRefReward"));
            sb.Append(Environment.NewLine); //new line

            // Column Detail
            foreach (var item in Data)
            {
                sb.Append(item.Username);
                sb.Append(separator);
                sb.Append(item.Date);
                sb.Append(separator);
                sb.Append(item.PlatformName);
                sb.Append(separator);
                sb.Append(item.Merchant_Ref_Reward);
                sb.Append(Environment.NewLine); //new line
            }

            var fileName = $"Transaction_MerchantReferralReward.csv";
            return File(Encoding.UTF8.GetBytes(sb.ToString()), MimeTypes.TextCsv, fileName);
        }

        #endregion

        #region Deposit Reward History
        public virtual IActionResult DepositRewardHistoryList() //Atiqah 20180927 MSP-150
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDepositRewardHistory))
                return AccessDeniedView();

            var model = new DepositRewardHistoryListModel();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult DepositRewardHistoryList(DataSourceRequest command, DepositRewardHistoryListModel model) //Atiqah 20180927 MSP-150
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDepositRewardHistory))
                return AccessDeniedKendoGridJson();

            var dtTo = _mspHelper.ToDate(model.DateTo); //RW 20181031 MSP-420

            var list = _transactionService.GetDepositRewardList(
                model.DateFrom,
                //dtTo, //RW 20181031 MSP-420
                model.DateTo, //Atiqah 20190221 MDT-257
                model.Username,
                //model.ContributedBy, //Jerry 20181009 MSP-209 //20190221 MDT-257
                model.DepositAmountFrom,
                model.DepositAmountTo,
                model.DepositReturnedFrom,
                model.DepositReturnedTo,
                //model.DepositRewardFrom, //Atiqah 20190221 MDT-257
                //model.DepositRewardTo, //Atiqah 20190221 MDT-257
                model.DepositReturnedRewardFrom,    //Atiqah 20190221 MDT-257
                model.DepositReturnedRewardTo,      //Atiqah 20190221 MDT-257
                model.DepositOverflowRewardFrom,    //Atiqah 20190221 MDT-257
                model.DepositOverflowRewardTo,      //Atiqah 20190221 MDT-257
                model.ExtraRewardFrom,
                model.ExtraRewardTo,
                model.TotalRewardFrom,
                model.TotalRewardTo,
                //Atiqah 20190221 MDT-257 \/
                //model.SelfScorePctFrom,
                //model.SelfScorePctTo,
                //model.SelfScoreFrom,
                //model.SelfScoreTo,
                //model.TeamScorePctFrom,
                //model.TeamScorePctTo,
                //model.TeamScoreFrom,
                //model.TeamScoreTo,
                //Atiqah 20190221 MDT-257 /\
                out int TotalRecord,
                out int TotalPage,
                pageIndex: command.Page,
                pageSize: command.PageSize
                );

            var gridModel = new DataSourceResult
            {
                Data = list.Select(x =>
                {
                    var _model = new DepositRewardHistoryModel
                    {
                        Username = x.Username,
                        Date = x.Date,
                        //ContributedBy = x.ContributedBy, //Atiqah 20190221 MDT-257
                        Deposit_Amt = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_Amt),
                        Deposit_Returned = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_Returned),
                        //Deposit_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_Reward), //Atiqah 20190221 MDT-257
                        Deposit_Returned_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_Returned_Reward), //Atiqah 20190221 MDT-257
                        Deposit_Overflow_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_Overflow_Reward), //Atiqah 20190221 MDT-257
                        Extra_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.Extra_Reward),
                        Total_Reward = _mspHelper.TruncateDecimalToString_MBTC(x.Total_Reward),
                        //Atiqah 20190221 MDT-257 \/
                        //SelfScore_Pct = _mspHelper.TruncateDecimalToString_MBTC(x.SelfScore_Pct),
                        //SelfScore = _mspHelper.TruncateDecimalToString_MBTC(x.SelfScore),
                        //TeamScore_Pct = _mspHelper.TruncateDecimalToString_MBTC(x.TeamScore_Pct),
                        //TeamScore = _mspHelper.TruncateDecimalToString_MBTC(x.TeamScore)
                        //Atiqah 20190221 MDT-257 /\
                    };
                    return _model;
                }),
                Total = TotalRecord
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("DepositRewardHistoryList")]
        [FormValueRequired("exportcsv")]
        public virtual IActionResult ExportDepositRewardHistoryListToCsv(DepositRewardHistoryListModel model) //Jerry 20181218 MDT-158
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDepositRewardHistory))
                return AccessDeniedKendoGridJson();

            var fileName = $"DepositRewardHistoryList_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}_{CommonHelper.GenerateRandomDigitCode(4)}.csv";

            var dtTo = _mspHelper.ToDate(model.DateTo);

            var list = _transactionService.GetDepositRewardList(
                model.DateFrom,
                //dtTo,  //Atiqah 20190221 MDT-257
                model.DateTo, //Atiqah 20190221 MDT-257
                model.Username,
                //model.ContributedBy, //Atiqah 20190221 MDT-257
                model.DepositAmountFrom,
                model.DepositAmountTo,
                model.DepositReturnedFrom,
                model.DepositReturnedTo,
                //model.DepositRewardFrom, //Atiqah 20190221 MDT-257
                //model.DepositRewardTo,   //Atiqah 20190221 MDT-257
                model.DepositReturnedRewardFrom,
                model.DepositReturnedRewardTo,
                model.DepositOverflowRewardFrom,
                model.DepositOverflowRewardTo,
                model.ExtraRewardFrom,
                model.ExtraRewardTo,
                model.TotalRewardFrom,
                model.TotalRewardTo,
                //Atiqah 20190221 MDT-257 \/
                //model.SelfScorePctFrom,
                //model.SelfScorePctTo,
                //model.SelfScoreFrom,
                //model.SelfScoreTo,
                //model.TeamScorePctFrom,
                //model.TeamScorePctTo,
                //model.TeamScoreFrom,
                //model.TeamScoreTo,
                //Atiqah 20190221 MDT-257 /\
                out int TotalRecord,
                out int TotalPage,
                pageIndex: 1,
                pageSize: int.MaxValue
                );

            var result = _exportManager.ExportDepositRewardHistoryListToTxt(list);

            return File(Encoding.UTF8.GetBytes(result), MimeTypes.TextCsv, fileName);
        }
        #endregion

        #endregion

    }
}
