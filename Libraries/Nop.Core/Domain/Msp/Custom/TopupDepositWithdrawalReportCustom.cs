using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Custom // Tony Liew 20180912 MSP-92
{
    /// <summary>
    /// Topup, Deposit ,Withdrawal Report Custom
    /// </summary>
    public class TopupDepositWithdrawalReportCustom
    {
        /// <summary>
        /// Topup Column
        /// </summary>
        public decimal Topup_mBTC { get; set; }

        /// <summary>
        /// Deposit Column
        /// </summary>
        public decimal Deposit_mBTC { get; set; }

        /// <summary>
        /// Net Withdraw Ammout Column
        /// </summary>
        public decimal Withdrawal_mBTC { get; set; }

        /// <summary>
        /// Net Balance Ammout Column
        /// </summary>
        public decimal Balance_mBTC { get; set; }


    }

    public class TopupDepositWithdrawalDailyReportCustom : TopupDepositWithdrawalReportCustom
    {
        /// <summary>
        /// Date Column
        /// </summary>
        public DateTime TrxDate { get; set; }
    }

    public class TopupDepositWithdrawalWeeklyReportCustom : TopupDepositWithdrawalReportCustom
    {
        /// <summary>
        /// Start Date Column
        /// </summary>
        public DateTime WeekStart { get; set; }

        /// <summary>
        /// End Date Column
        /// </summary>
        public DateTime WeekEnd { get; set; }
    }

    public class TopupDepositWithdrawalMonthlyReportCustom : TopupDepositWithdrawalReportCustom
    {
        /// <summary>
        /// Month Column
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Year Column
        /// </summary>
        public int Year { get; set; }
    }
}
