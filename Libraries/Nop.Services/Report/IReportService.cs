using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Msp.Custom;
//using Nop.Core.Domain.Msp.Security; //Atiqah 20190131 MDT-205
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Report
{
    public interface IReportService
    {
        /// <summary>
        /// Get Monthly Topup, Deposit, Withdrawal Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        IList<TopupDepositWithdrawalMonthlyReportCustom> MonthlyList
            (
            DateTime dateFrom
            , DateTime dateTo
            , out int TotalRecords
            , out int TotalPages
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            );//Tony Liew 20180828 MSP-92

        /// <summary>
        /// Get Weekly Topup, Deposit, Withdrawal Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        IList<TopupDepositWithdrawalWeeklyReportCustom> WeeklyList
            (
            DateTime dateFrom
            , DateTime dateTo
            , out int TotalRecords
            , out int TotalPages
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            );//Tony Liew 20180828 MSP-92

        /// <summary>
        /// Get Daily Topup, Deposit, Withdrawal Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        IList<TopupDepositWithdrawalDailyReportCustom> DailyList
            (
            DateTime dateFrom
            , DateTime dateTo
            , out int TotalRecords
            , out int TotalPages
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            );//Tony Liew 20180828 MSP-92

        /// <summary>
        /// Get Daily Deposit, Deposit Returned, Consumption Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        IList<DepositDepositReturnedConsumptionDailyReportCustom> DailyDepositList(
              out int TotalRecord
            , out int TotalPage
            , DateTime dateFrom
            , DateTime dateTo
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            );//Atiqah 20180919 MSP-92


        /// <summary>
        /// Get Weekly Deposit, Deposit Returned, Consumption Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        IList<DepositDepositReturnedConsumptionWeeklyReportCustom> WeeklyDepositList(
              out int TotalRecord
            , out int TotalPage
            , DateTime dateFrom
            , DateTime dateTo
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            );//Atiqah 20180919 MSP-92

        /// <summary>
        /// Get Monthly Deposit, Deposit Returned, Consumption Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        IList<DepositDepositReturnedConsumptionMonthlyReportCustom> MonthlyDepositList(
              out int TotalRecord
            , out int TotalPage
            , DateTime dateFrom
            , DateTime dateTo
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            );//Atiqah 20180919 MSP-92

    }
}
