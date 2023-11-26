using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Customers;
using Nop.Services.ExportImport;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Report;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Models.Report;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using System;
using System.Linq;
using System.Text;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class ReportController : BaseAdminController
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IReportService _reportService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IMspHelper _mspHelper;
        private readonly IExportManager _exportManager; //Jerry 20181219 MDT-159

        #endregion

        #region Ctor

        public ReportController
            (ILogger logger
            , IWorkContext workContext
            , IPermissionService permissionService
            , ICustomerService customerService
            , ILocalizationService localizationService
            //, ISecurityQuestionManagementService securityQuestionManagementService //Atiqah 20190131 MDT-205
            , ICustomerActivityService customerActivityService
            , IReportService reportService
            , IMspHelper mspHelper
            , IExportManager exportManager //Jerry 20181219 MDT-159
            )
        {
            this._logger = logger;
            this._workContext = workContext;
            this._permissionService = permissionService;
            this._customerService = customerService;
            this._localizationService = localizationService;
            this._reportService = reportService;
            this._customerActivityService = customerActivityService;
            this._mspHelper = mspHelper;
            this._exportManager = exportManager; //Jerry 20181219 MDT-159
        }

        #endregion

        #region Backend Function: Report

        #region Top Up, Deposit and Withdrawal
        //Tony Liew 20180920 MSP-92 \/
        public virtual IActionResult Index()
        {
            return RedirectToAction("WithdrawalList");
        }
        //Tony Liew 20180920 MSP-92 /\

        //Tony Liew 20180920 MSP-92 \/
        public virtual IActionResult WithdrawalList()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageTopUpDepositWithdrawalReport))
                return AccessDeniedView();

            var model = new ReportListModel();

            return View(model);
        }
        //Tony Liew 20180920 MSP-92 /\

        //Tony Liew 20180920 MSP-92 \/
        [HttpPost]
        public virtual IActionResult WithdrawalList(DataSourceRequest command, ReportListModel model) //Tony Liew 20180828 MSP-92
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageTopUpDepositWithdrawalReport))
                return AccessDeniedKendoGridJson();

            string dateformat = _mspHelper.GetSettingValueByKey("MSP_GlobalUIDateFormat", "yyyy-MM-dd"); //RW 20180712 MDT-128

            var gridModel = new DataSourceResult();
            if (model.Frequency == 1)
            {
                var daily = _reportService.DailyList(
                    model.DateFrom
                    , model.DateTo
                    , out int TotalRecord
                    , out int TotalPage
                    , pageNumber: command.Page - 1 //RW MSP-233 20181005
                    , pageSize: command.PageSize
                    );
                gridModel = new DataSourceResult
                {
                    Data = daily.Select(x =>
                    {
                        var _model = new ReportModel
                        {
                            MBTC_Topup = _mspHelper.TruncateDecimalToString_MBTC(x.Topup_mBTC), //RW 20180712 MDT-128 
                            MBTC_Withdrawal = _mspHelper.TruncateDecimalToString_MBTC(x.Withdrawal_mBTC), //RW 20180712 MDT-128 
                            MBTC_Deposit = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_mBTC), //RW 20180712 MDT-128 
                            Date = x.TrxDate.ToString(dateformat), //RW 20180712 MDT-128
                            MBTC_Balance = _mspHelper.TruncateDecimalToString_MBTC((x.Topup_mBTC - x.Deposit_mBTC - x.Withdrawal_mBTC)) //RW 20180712 MDT-128 
                        };
                        return _model;
                    }),
                    Total = TotalRecord
                };
                return Json(gridModel);
            }
            else if (model.Frequency == 2)
            {
                var weekly = _reportService.WeeklyList(
                    model.DateFrom
                    , model.DateTo
                    , out int TotalRecord
                    , out int TotalPage
                    , pageNumber: command.Page - 1 //RW MSP-233 20181005
                    , pageSize: command.PageSize
                    );
                gridModel = new DataSourceResult
                {
                    Data = weekly.Select(x =>
                    {
                        var _model = new ReportModel
                        {
                            MBTC_Topup = _mspHelper.TruncateDecimalToString_MBTC(x.Topup_mBTC), //RW 20180712 MDT-128 
                            MBTC_Withdrawal = _mspHelper.TruncateDecimalToString_MBTC(x.Withdrawal_mBTC), //RW 20180712 MDT-128 
                            MBTC_Deposit = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_mBTC), //RW 20180712 MDT-128 
                            MBTC_Balance = _mspHelper.TruncateDecimalToString_MBTC((x.Topup_mBTC - x.Deposit_mBTC - x.Withdrawal_mBTC)), //RW 20180712 MDT-128 
                            StartDate = x.WeekStart.ToString(dateformat), //RW 20180712 MDT-128 
                            EndDate = x.WeekEnd.ToString(dateformat) //RW 20180712 MDT-128 
                        };
                        return _model;
                    }),
                    Total = TotalRecord
                };
                return Json(gridModel);
            }
            else if (model.Frequency == 3)
            {
                var monthly = _reportService.MonthlyList(
                    model.DateFrom
                    , model.DateTo
                    , out int TotalRecord
                    , out int TotalPage
                    , pageNumber: command.Page - 1 //RW MSP-233 20181005
                    , pageSize: command.PageSize
                    );
                gridModel = new DataSourceResult
                {
                    Data = monthly.Select(x =>
                    {
                        var _model = new ReportModel
                        {
                            MBTC_Topup = _mspHelper.TruncateDecimalToString_MBTC(x.Topup_mBTC), //RW 20180712 MDT-128 
                            MBTC_Withdrawal = _mspHelper.TruncateDecimalToString_MBTC(x.Withdrawal_mBTC), //RW 20180712 MDT-128 
                            MBTC_Deposit = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_mBTC), //RW 20180712 MDT-128 
                            MBTC_Balance = _mspHelper.TruncateDecimalToString_MBTC((x.Topup_mBTC - x.Deposit_mBTC - x.Withdrawal_mBTC)), //RW 20180712 MDT-128 
                            Month = x.Month.ToString(),
                            Year = x.Year.ToString()
                        };
                        return _model;
                    }),
                    Total = TotalRecord
                };
                return Json(gridModel);
            }
            else
                return Json(gridModel);

        }
        //Tony Liew 20180920 MSP-92/\

        [HttpPost, ActionName("WithdrawalList")]
        [FormValueRequired("exportcsv")]
        public virtual IActionResult ExportWithdrawalListToCsv(ReportListModel model) //Jerry 20181219 MDT-159
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageTopUpDepositWithdrawalReport))
                return AccessDeniedKendoGridJson();

            string result = "";
            var fileName = $"TopUp_AgencyFee_Withdrawal_<>_Report_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}_{CommonHelper.GenerateRandomDigitCode(4)}.csv";

            if (model.Frequency == 1)
            {
                var daily = _reportService.DailyList(
                    model.DateFrom
                    , model.DateTo
                    , out int TotalRecord
                    , out int TotalPage
                    , pageNumber: 1
                    , pageSize: int.MaxValue
                    );
                result = _exportManager.ExportTopUpDepositWithdrawalReportToTxt(daily);
                fileName = fileName.Replace("<>", "Daily");
            }
            else if (model.Frequency == 2)
            {
                var weekly = _reportService.WeeklyList(
                    model.DateFrom
                    , model.DateTo
                    , out int TotalRecord
                    , out int TotalPage
                    , pageNumber: 1
                    , pageSize: int.MaxValue
                    );
                result = _exportManager.ExportTopUpDepositWithdrawalReportToTxt(weekly);
                fileName = fileName.Replace("<>", "Weekly");
            }
            else if (model.Frequency == 3)
            {
                var monthly = _reportService.MonthlyList(
                    model.DateFrom
                    , model.DateTo
                    , out int TotalRecord
                    , out int TotalPage
                    , pageNumber: 1
                    , pageSize: int.MaxValue
                    );
                result = _exportManager.ExportTopUpDepositWithdrawalReportToTxt(monthly);
                fileName = fileName.Replace("<>", "Monthly");
            }

            return File(Encoding.UTF8.GetBytes(result), MimeTypes.TextCsv, fileName);
        }
        #endregion

        #region Deposit, Deposit Returned and Consumption
        public virtual IActionResult ConsumptionIndex() //Atiqah 20180919 MSP-92
        {
            return RedirectToAction("ConsumptionList");
        }

        public virtual IActionResult ConsumptionList() //Atiqah 20180919 MSP-92
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDepositDepositReturnedConsumptionReport))
                return AccessDeniedView();

            var model = new ReportListModel();

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ConsumptionList(DataSourceRequest command, ReportListModel model) //Atiqah 20180919 MSP-92
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDepositDepositReturnedConsumptionReport))
                return AccessDeniedKendoGridJson();

            string dateformat = _mspHelper.GetSettingValueByKey("MSP_GlobalUIDateFormat", "yyyy-MM-dd"); //RW 20180712 MDT-128

            var gridModel = new DataSourceResult();
            if (model.Frequency == 1)
            {
                var daily = _reportService.DailyDepositList(
                    out int TotalRecord
                    , out int TotalPage
                    , model.DateFrom
                    , model.DateTo
                    , pageNumber: command.Page
                    , pageSize: command.PageSize
                    );
                gridModel = new DataSourceResult
                {
                    Data = daily.Select(x =>
                    {
                        var _model = new ReportModel
                        {
                            Date = x.TrxDate.ToString(dateformat), //RW 20180712 MDT-128
                            MBTC_Deposit = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_Amt), //RW 20180712 MDT-128
                            MBTC_Deposit_Return = _mspHelper.TruncateDecimalToString_MBTC(x.Offset_Amt), //RW 20180712 MDT-128
                            MBTC_Consumption = _mspHelper.TruncateDecimalToString_MBTC(x.Consumption_Amt) //RW 20180712 MDT-128
                        };
                        return _model;
                    }),
                    Total = TotalRecord
                };
                return Json(gridModel);
            }
            else if (model.Frequency == 2)
            {
                var weekly = _reportService.WeeklyDepositList(
                    out int TotalRecord
                    , out int TotalPage
                    , model.DateFrom
                    , model.DateTo
                    , pageNumber: command.Page
                    , pageSize: command.PageSize
                    );
                gridModel = new DataSourceResult
                {
                    Data = weekly.Select(x =>
                    {
                        var _model = new ReportModel
                        {
                            StartDate = x.WeekStart.ToString(dateformat), //RW 20180712 MDT-128
                            EndDate = x.WeekEnd.ToString(dateformat), //RW 20180712 MDT-128
                            MBTC_Deposit = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_Amt), //RW 20180712 MDT-128
                            MBTC_Deposit_Return = _mspHelper.TruncateDecimalToString_MBTC(x.Offset_Amt), //RW 20180712 MDT-128
                            MBTC_Consumption = _mspHelper.TruncateDecimalToString_MBTC(x.Consumption_Amt) //RW 20180712 MDT-128
                        };
                        return _model;
                    }),
                    Total = TotalRecord
                };
                return Json(gridModel);
            }
            else if (model.Frequency == 3)
            {
                var monthly = _reportService.MonthlyDepositList(
                    out int TotalRecord
                    , out int TotalPage
                    , model.DateFrom
                    , model.DateTo
                    , pageNumber: command.Page - 1
                    , pageSize: command.PageSize
                    );
                gridModel = new DataSourceResult
                {
                    Data = monthly.Select(x =>
                    {
                        var _model = new ReportModel
                        {
                            Month = x.Month.ToString(),
                            Year = x.Year.ToString(),
                            MBTC_Deposit = _mspHelper.TruncateDecimalToString_MBTC(x.Deposit_Amt), //RW 20180712 MDT-128
                            MBTC_Deposit_Return = _mspHelper.TruncateDecimalToString_MBTC(x.Offset_Amt), //RW 20180712 MDT-128
                            MBTC_Consumption = _mspHelper.TruncateDecimalToString_MBTC(x.Consumption_Amt) //RW 20180712 MDT-128
                        };
                        return _model;
                    }),
                    Total = TotalRecord
                };
                return Json(gridModel);
            }
            else
                return Json(gridModel);
        }

        [HttpPost, ActionName("ConsumptionList")]
        [FormValueRequired("exportcsv")]
        public virtual IActionResult ExportConsumptionListToCsv(ReportListModel model) //Jerry 20181219 MDT-160
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageDepositDepositReturnedConsumptionReport))
                return AccessDeniedKendoGridJson();

            string result = "";
            var fileName = $"AgencyFee_AgencyFeeReturned_Task_<>_Report_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}_{CommonHelper.GenerateRandomDigitCode(4)}.csv";

            if (model.Frequency == 1)
            {
                var daily = _reportService.DailyDepositList(
                    out int TotalRecord
                    , out int TotalPage
                    , model.DateFrom
                    , model.DateTo
                    , pageNumber: 1
                    , pageSize: int.MaxValue
                    );
                result = _exportManager.ExportDepositDepositReturnedConsumptionReportToTxt(daily);
                fileName = fileName.Replace("<>", "Daily");
            }
            else if (model.Frequency == 2)
            {
                var weekly = _reportService.WeeklyDepositList(
                   out int TotalRecord
                   , out int TotalPage
                   , model.DateFrom
                   , model.DateTo
                   , pageNumber: 1
                   , pageSize: int.MaxValue
                   );
                result = _exportManager.ExportDepositDepositReturnedConsumptionReportToTxt(weekly);
                fileName = fileName.Replace("<>", "Weekly");
            }
            else if (model.Frequency == 3)
            {
                var monthly = _reportService.MonthlyDepositList(
                    out int TotalRecord
                    , out int TotalPage
                    , model.DateFrom
                    , model.DateTo
                    , pageNumber: 1
                    , pageSize: int.MaxValue
                    );
                result = _exportManager.ExportDepositDepositReturnedConsumptionReportToTxt(monthly);
                fileName = fileName.Replace("<>", "Monthly");
            }

            return File(Encoding.UTF8.GetBytes(result), MimeTypes.TextCsv, fileName);
        }
        #endregion

        #endregion
    }
}