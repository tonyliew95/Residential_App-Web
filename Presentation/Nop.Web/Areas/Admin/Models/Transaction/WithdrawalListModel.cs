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
    public class WithdrawalListModel : BaseNopModel //wailiang 20180910 MSP-95
    {
        public WithdrawalListModel()
        {
            //this.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            //this.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
            Status = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.TxnId")]
        public int? TxnNo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.Amount")]
        public decimal? Amount { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.TransactionFees")]
        public decimal? TransactionFees { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.NetAmount")]
        public decimal? NetAmount { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.DateFrom")]
        [UIHint("DateFrom")]
        public DateTime? DateFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.DateTo")]
        [UIHint("DateTo")]
        public DateTime? DateTo { get; set; }

        //wailiang 20190116 MDT-195 \/
        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.WithdrawalAddress")]
        public string WithdrawalAddress { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.BlockchainTxId")]
        public string BlockChainTxId { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.Status")]
        public string StatusValue { get; set; }
        public IList<SelectListItem> Status { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.List.RefundStatus")]
        public string RefundStatus { get; set; }
        
        public string Remark { get; set; }
        //wailiang 20190116 MDT-195 /\
    }

}
