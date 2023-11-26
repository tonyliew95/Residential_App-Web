using Nop.Core.Enumeration;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class DepositModel : BaseNopModel
    {

        public DepositModel()
        {
        }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.Fields.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.Fields.Date")]
        public DateTime Date { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.Fields.TransactionNo")]
        public int? TxnNo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.Fields.DepositAmount")]
        public decimal DepositAmt { get; set; }

        //wailiang 20190115 MDT-194 \/
        //[NopResourceDisplayName("Admin.TransactionList.Deposit.Fields.Remark")]
        //public string Remark { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Deposit.Fields.Status")]
        public string Status { get; set; }
        //wailiang 20190115 MDT-194 /\
    }
}
