using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Report
{
    public class ReportListModel : BaseNopModel //Tony Liew 20180910 MSP-92
    {
        public List<FrequencyModel> ReportfrequencyList;

        public ReportListModel()
        {
            ReportfrequencyList = new List<FrequencyModel>();
            ReportfrequencyList.Add(new FrequencyModel("Admin.Report.List.IsDaily", 1, true));
            ReportfrequencyList.Add(new FrequencyModel("Admin.Report.List.IsWeekly", 2, false));
            ReportfrequencyList.Add(new FrequencyModel("Admin.Report.List.IsMonthly", 3, false));

            DateTime dt = DateTime.Now;

            this.DateFrom = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
            this.DateTo = new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59);
        }

        [NopResourceDisplayName("Admin.Report.List.DateStart")]
        public DateTime DateFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.DateTo")]
        public DateTime DateTo { get; set; }

        [NopResourceDisplayName("Admin.Report.List.Frequency")]
        public int Frequency { get; set; }

        public class FrequencyModel
        {
            public string FrequencyName { get; set; }
            public int FrequencyValue { get; set; }
            public bool FrequencyIsSelected { get; set; }

            public FrequencyModel(string freqName, int freqVal, bool freqIsSelected)
            {
                FrequencyName = freqName;
                FrequencyValue = freqVal;
                FrequencyIsSelected = freqIsSelected; 
            }
        }
    }
   
   
}