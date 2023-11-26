using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Report
{
    public class ReportModel : BaseNopModel //Tony Liew 20180910 MSP-92
    {
        public ReportModel()
        {
        }
        [NopResourceDisplayName("Admin.Report.Fields.StartDate")]
        public string StartDate { get; set; }

        [NopResourceDisplayName("Admin.Report.Fields.EndDate")]
        public string EndDate { get; set; }

        [NopResourceDisplayName("Admin.Report.Fields.Date")]
        public string Date { get; set; }

        [NopResourceDisplayName("Admin.Report.Fields.Month")]
        public string Month { get; set; }

        [NopResourceDisplayName("Admin.Report.Fields.Year")]
        public string Year { get; set; }

        [NopResourceDisplayName("Admin.Report.Fields.MbtcDeposit")]
        public string MBTC_Deposit { get; set; }

        [NopResourceDisplayName("Admin.Report.DepositDepositReturnedConsumption.Fields.MbtcDepositReturn")]
        public string MBTC_Deposit_Return { get; set; }

        [NopResourceDisplayName("Admin.Report.DepositDepositReturnedConsumption.Fields.MbtcConsumption")]
        public string MBTC_Consumption { get; set; }

        [NopResourceDisplayName("Admin.Report.TopUpDepositWithdrawal.Fields.MbtcTopup")]
        public string MBTC_Topup { get; set; }

        [NopResourceDisplayName("Admin.Report.TopUpDepositWithdrawal.Fields.MbtcWithdrawal")]
        public string MBTC_Withdrawal { get; set; }

        [NopResourceDisplayName("Admin.Report.TopUpDepositWithdrawal.Fields.MbtcBalance")]
        public string MBTC_Balance { get; set; }
    }
    
}
