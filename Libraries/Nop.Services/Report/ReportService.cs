using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Msp.Custom;
//using Nop.Core.Domain.Msp.Security; //Atiqah 20190131 MDT-205
using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Enumeration;
using Nop.Data;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Report
{
    public partial class ReportService : IReportService
    { 
        private readonly IRepository<MSP_Mbtc_Deposit> _mbtc_DepositRepository;
        private readonly IRepository<MSP_Deposit> _depositRepository;
        private readonly IRepository<MSP_Mbtc_Withdrawal> _mbtc_WithdrawalRepository;
        private readonly IDbContext _dbContext;

        public ReportService
        (
             IRepository<MSP_Mbtc_Deposit> mbtc_DepositRepository
            , IRepository<MSP_Deposit> depositRepository
            , IRepository<MSP_Mbtc_Withdrawal> mbtc_WithdrawalRepository
            , IDbContext dbContext
        )
        {
            _mbtc_DepositRepository = mbtc_DepositRepository;
            _depositRepository = depositRepository;
            _mbtc_WithdrawalRepository = mbtc_WithdrawalRepository;
            _dbContext = dbContext;
        }

        #region Backend Function: Report

        #region Top Up, Deposit and Withdrawal
        /// <summary>
        /// Get Daily Topup, Deposit, Withdrawal Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public IList<TopupDepositWithdrawalDailyReportCustom> DailyList
            (
            DateTime dateFrom
            , DateTime dateTo
            , out int TotalRecords
            , out int TotalPages
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            ) //Tony Liew 20180925 MSP-92
        {
            TotalPages = 0;
            TotalRecords = 0;
            #region Output Variable
            SqlParameter @outReturnCode = new SqlParameter()
            {
                ParameterName = "@outReturnCode",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };

            SqlParameter @outReturnMessage = new SqlParameter()
            {
                ParameterName = "@outReturnMessage",
                SqlDbType = SqlDbType.NVarChar,
                Size = 200,
                Direction = ParameterDirection.Output,
                Value = 0
            };

            SqlParameter @outTotalRecords = new SqlParameter()
            {
                ParameterName = "@outTotalRecords",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };

            SqlParameter @outTotalPages = new SqlParameter()
            {
                ParameterName = "@outTotalPages",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };
            #endregion

            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_Report_Topup_Deposit_Withdrawal_Daily]");
            sp.Append(" @FromDate");
            sp.Append(",@ToDate");
            sp.Append(",@pageNumber");
            sp.Append(",@pageSize");
            sp.Append(",@outTotalRecords OUT");
            sp.Append(",@outTotalPages OUT");
            sp.Append(",@outReturnCode OUT");
            sp.Append(",@outReturnMessage OUT");

            var returnList = _dbContext.SqlQuery<TopupDepositWithdrawalDailyReportCustom>(
                sp.ToString()
                , new SqlParameter("@FromDate", dateFrom)
                , new SqlParameter("@ToDate", dateTo)
                , new SqlParameter("@pageNumber", pageNumber)
                , new SqlParameter("@pageSize", pageSize)
                , outTotalRecords
                , outTotalPages
                , outReturnCode
                , outReturnMessage
                ).ToList();

            //RW MSP-233 20181005
            if (returnList.Count > 0)
            {
                TotalPages = Convert.ToInt32(outTotalPages.SqlValue.ToString());
                TotalRecords = Convert.ToInt32(outTotalRecords.SqlValue.ToString());
            }
            //RW MSP-233 20181005

            return returnList;
        }
        /// <summary>
        /// Get Weekly Topup, Deposit, Withdrawal Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public IList<TopupDepositWithdrawalWeeklyReportCustom> WeeklyList
            (DateTime dateFrom
            , DateTime dateTo
            , out int TotalRecords
            , out int TotalPages
            , int PageNo = 0
            , int pageSize = int.MaxValue) //Tony Liew 20180925 MSP-92
        {
            TotalPages = 0;
            TotalRecords = 0;
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

            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_Report_Topup_Deposit_Withdrawal_Weekly]");
            sp.Append(" @FromDate");
            sp.Append(",@ToDate");
            sp.Append(",@pageNumber");
            sp.Append(",@pageSize");
            sp.Append(",@outTotalRecords OUT");
            sp.Append(",@outTotalPages OUT");
            sp.Append(",@outReturnCode OUT");
            sp.Append(",@outReturnMessage OUT");

            var returnList = _dbContext.SqlQuery<TopupDepositWithdrawalWeeklyReportCustom>(
                sp.ToString()
                , new SqlParameter("@FromDate", dateFrom)
                , new SqlParameter("@ToDate", dateTo)
                , new SqlParameter("@pageNumber", PageNo)
                , new SqlParameter("@pageSize", pageSize)
                , outTotalRecords
                , outTotalPages
                , outReturnCode
                , outReturnMessage
                ).ToList();
            if (returnList.Count > 0)
            {
                TotalPages = Convert.ToInt32(outTotalPages.SqlValue.ToString());
                TotalRecords = Convert.ToInt32(outTotalRecords.SqlValue.ToString());
            }

            return returnList;
        }

        /// <summary>
        /// Get Monthly Topup, Deposit, Withdrawal Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public IList<TopupDepositWithdrawalMonthlyReportCustom> MonthlyList            
            (DateTime dateFrom
            , DateTime dateTo
            , out int TotalRecords
            , out int TotalPages
            , int PageNo = 0
            , int pageSize = int.MaxValue) //Tony Liew 20180925 MSP-92
        {
            TotalPages = 0;
            TotalRecords = 0;
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

            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_Report_Topup_Deposit_Withdrawal_Monthly]");
            sp.Append(" @FromDate");
            sp.Append(",@ToDate");
            sp.Append(",@pageNumber");
            sp.Append(",@pageSize");
            sp.Append(",@outTotalRecords OUT");
            sp.Append(",@outTotalPages OUT");
            sp.Append(",@outReturnCode OUT");
            sp.Append(",@outReturnMessage OUT");

            var returnList = _dbContext.SqlQuery<TopupDepositWithdrawalMonthlyReportCustom>(
                sp.ToString()
                , new SqlParameter("@FromDate", dateFrom)
                , new SqlParameter("@ToDate", dateTo)
                , new SqlParameter("@pageNumber", PageNo)
                , new SqlParameter("@pageSize", pageSize)
                , outTotalRecords
                , outTotalPages
                , outReturnCode
                , outReturnMessage
                ).ToList();

            if (returnList.Count > 0)
            {
                TotalPages = Convert.ToInt32(outTotalPages.SqlValue.ToString());
                TotalRecords = Convert.ToInt32(outTotalRecords.SqlValue.ToString());
            }
            return returnList;
        }
        #endregion

        #region Deposit, Deposit Returned and Consumption
        #region Daily

        /// <summary>
        /// Get Daily Deposit, Deposit Returned, Consumption Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public IList<DepositDepositReturnedConsumptionDailyReportCustom> DailyDepositList(
              out int TotalRecord
            , out int TotalPage
            , DateTime dateFrom
            , DateTime dateTo
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            )//Atiqah 20180919 MSP-92
        {
            TotalRecord = 0;
            TotalPage = 0;

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

            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_Report_Deposit_DepositReturned_Consumption_Daily]");
            sp.Append(" @FromDate");
            sp.Append(",@ToDate");
            sp.Append(",@PageNumber");
            sp.Append(",@PageSize");
            sp.Append(",@outTotalRecords OUT");
            sp.Append(",@outTotalPages OUT");
            sp.Append(",@outReturnCode OUT");
            sp.Append(",@outReturnMessage OUT");


            var returnList = _dbContext.SqlQuery<DepositDepositReturnedConsumptionDailyReportCustom>(
                sp.ToString()
                , new SqlParameter("@FromDate", dateFrom)
                , new SqlParameter("@ToDate", dateTo)
                , new SqlParameter("@PageNumber", pageNumber)
                , new SqlParameter("@PageSize", pageSize)
                , outTotalRecords
                , outTotalPages
                , outReturnCode
                , outReturnMessage
                ).ToList();

            //var getDailyReturn = new PagedList<DepositDepositReturnedConsumptionDailyReportCustom>(returnList, pageNumber, pageSize);//Atiqah 20180928 MSP-92

            if (returnList.Count > 0)
            {
                TotalPage = Convert.ToInt32(outTotalPages.SqlValue.ToString());
                TotalRecord = Convert.ToInt32(outTotalRecords.SqlValue.ToString());
            }

            return returnList;
        }
        #endregion

        #region Weekly

        /// <summary>
        /// Get Weekly Deposit, Deposit Returned, Consumption Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public IList<DepositDepositReturnedConsumptionWeeklyReportCustom> WeeklyDepositList(
              out int TotalRecord
            , out int TotalPage
            , DateTime dateFrom
            , DateTime dateTo
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            )//Atiqah 20180919 MSP-92
        {
            TotalRecord = 0;
            TotalPage = 0;
            #region Output Variable
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

            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_Report_Deposit_DepositReturned_Consumption_Weekly]");
            sp.Append(" @FromDate");
            sp.Append(",@ToDate");
            sp.Append(",@PageNumber");
            sp.Append(",@PageSize");
            sp.Append(",@outTotalRecords OUT");
            sp.Append(",@outTotalPages OUT");

            var returnList = _dbContext.SqlQuery<DepositDepositReturnedConsumptionWeeklyReportCustom>(
                sp.ToString()
                , new SqlParameter("@FromDate", dateFrom)
                , new SqlParameter("@ToDate", dateTo)
                , new SqlParameter("@PageNumber", pageNumber)
                , new SqlParameter("@PageSize", pageSize)
                , outTotalRecords
                , outTotalPages
                ).ToList();

            //var getWeeklyReturn = new PagedList<DepositDepositReturnedConsumptionWeeklyReportCustom>(returnList, pageNumber, pageSize);//Atiqah 20180928 MSP-92

            if (returnList.Count > 0)
            {
                TotalPage = Convert.ToInt32(outTotalPages.SqlValue.ToString());
                TotalRecord = Convert.ToInt32(outTotalRecords.SqlValue.ToString());
            }

            return returnList;
        }
        #endregion

        #region Monthly
        /// <summary>
        /// Get Monthly Deposit, Deposit Returned, Consumption Report
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public IList<DepositDepositReturnedConsumptionMonthlyReportCustom> MonthlyDepositList(
              out int TotalRecord
            , out int TotalPage
            , DateTime dateFrom
            , DateTime dateTo
            , int pageNumber = 0
            , int pageSize = int.MaxValue
            )//Atiqah 20180919 MSP-92
        {
            TotalRecord = 0;
            TotalPage = 0;

            #region Output Variable
            SqlParameter @outTotalRecords = new SqlParameter()
            {
                ParameterName = "@outTotalRecords",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };

            SqlParameter @outTotalPages = new SqlParameter()
            {
                ParameterName = "@outTotalPages",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output,
                Value = 0
            };
            #endregion

            StringBuilder sp = new StringBuilder();
            sp.Append("EXEC [usp_MSP_BE_Report_Deposit_DepositReturned_Consumption_Monthly]");
            sp.Append(" @FromDate");
            sp.Append(",@ToDate");
            sp.Append(",@PageNumber");
            sp.Append(",@PageSize");
            sp.Append(",@outTotalRecords OUT");
            sp.Append(",@outTotalPages OUT");

            var returnList = _dbContext.SqlQuery<DepositDepositReturnedConsumptionMonthlyReportCustom>(
                sp.ToString()
                , new SqlParameter("@FromDate", dateFrom.ToString("yyyyMMdd"))
                , new SqlParameter("@ToDate", dateTo.ToString("yyyyMMdd"))
                , new SqlParameter("@PageNumber", pageNumber)
                , new SqlParameter("@PageSize", pageSize)
                , outTotalRecords
                , outTotalPages
                ).ToList();

            //var getMonthlyReturn = new PagedList<DepositDepositReturnedConsumptionMonthlyReportCustom>(returnList, pageNumber, pageSize);//Atiqah 20180928 MSP-92

            if (returnList.Count > 0)
            {
                TotalPage = Convert.ToInt32(outTotalPages.SqlValue.ToString());
                TotalRecord = Convert.ToInt32(outTotalRecords.SqlValue.ToString());
            }

            return returnList;
        }
        #endregion

        #endregion

        #endregion

    }
}
