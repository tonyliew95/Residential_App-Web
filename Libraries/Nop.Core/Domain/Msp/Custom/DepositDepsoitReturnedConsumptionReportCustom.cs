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
    public class DepositDepositReturnedConsumptionReportCustom
    {
        /// <summary>
        /// Topup Column
        /// </summary>
        public decimal Deposit_Amt { get; set; }

        /// <summary>
        /// Deposit Column
        /// </summary>
        public decimal Offset_Amt { get; set; }

        /// <summary>
        /// Net Withdraw Ammout Column
        /// </summary>
        public decimal Consumption_Amt { get; set; }
    }

    public class DepositDepositReturnedConsumptionDailyReportCustom : DepositDepositReturnedConsumptionReportCustom
    {
        /// <summary>
        /// Date Column
        /// </summary>
        public DateTime TrxDate { get; set; }
    }

    public class DepositDepositReturnedConsumptionWeeklyReportCustom : DepositDepositReturnedConsumptionReportCustom
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

    public class DepositDepositReturnedConsumptionMonthlyReportCustom : DepositDepositReturnedConsumptionReportCustom
    {
        /// <summary>
        /// Month Column
        /// </summary>
        public int Month { get; set; }

        public int Year { get; set; }
    }
}
