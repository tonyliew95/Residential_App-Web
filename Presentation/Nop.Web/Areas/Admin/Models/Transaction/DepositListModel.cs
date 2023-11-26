using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class DepositListModel : BaseNopModel
    {
        public DepositListModel()
        {
            this.DepositTxnList = new List<DepositModel>();
            //wailiang 20181002 MSP-190 \/
            //this.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            //this.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
            //wailiang 20181002 MSP-190 /\
            Status = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.List.Username")]
        public string Username { get; set; }
        public bool UsernamesEnabled { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.List.DateFrom")]
        //[UIHint("DateNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateFrom")] //wailiang 20181002 MSP-190
        public DateTime? DateFrom { get; set; }
        public bool DateTimeEnabled { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.List.DateTo")]
        //[UIHint("DateNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTo")] //wailiang 20181002 MSP-190
        public DateTime? DateTo { get; set; }
        public bool DateToEnabled { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.List.TransactionNo")]
        public int? TxnNo { get; set; }
        public bool TxnNoEnabled { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.List.DepositAmount")]
        public decimal? DepositAmt { get; set; }
        public bool DepositAmtEnabled { get; set; }

        //wailiang 20190115 MDT-194 \/

        //[NopResourceDisplayName("Admin.TransactionList.Deposit.List.Remark")]
        //public string Remark { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.List.Status")]
        public string StatusValue { get; set; }
        public IList<SelectListItem> Status { get; set; }
        //wailiang 20190115 MDT-194 /\

        public IList<DepositModel> DepositTxnList { get; set; }
    }

}
