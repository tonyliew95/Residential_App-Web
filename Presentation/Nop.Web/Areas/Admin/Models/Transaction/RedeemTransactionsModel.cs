using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Msp.Transaction;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class RedeemTransactionsModel : BaseNopModel //JK 20180912 MSP-96
    {
        public RedeemTransactionsModel()
        {
        }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.Fields.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.Fields.WalletID")]
        public string WalletID { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.Fields.AvailableBalance")]
        public string AvailableBalance { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.Fields.RedeemWalletBalance")]
        public string RedeemWalletBalance { get; set; }
    }
}
