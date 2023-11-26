using FluentValidation.Attributes;
using Nop.Web.Areas.Admin.Validators.Withdrawal;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    [Validator(typeof(WithdrawalValidator))] //wailiang 20181031 MSP-423
    public class WithdrawalModel : BaseNopModel //wailiang 20180910 MSP-95
    {

        public WithdrawalModel()
        {
        }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.Date")]
        public DateTime Date { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.TxnId")]
        public int TxnNo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.Amount.mbtc")]
        public string Amount { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.TransactionFees.mbtc")]
        public string TransactionFees { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.NetAmount.mbtc")]
        public string NetAmount { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.WithdrawalAddress")]
        public string WithdrawalAddress { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.BlockchainTxId")]
        public string BlockChainTxId { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.Status")]
        public string Status { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.RefundStatus")]
        public string RefundStatus { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.Withdrawal.Fields.Remark")]
        public string Remark { get; set; }
        
        
    }
}
