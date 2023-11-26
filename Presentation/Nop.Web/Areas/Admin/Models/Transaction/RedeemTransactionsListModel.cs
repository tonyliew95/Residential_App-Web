using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public partial class RedeemTransactionsListModel : BaseNopModel //JK 20180912 MSP-96
    {
        public RedeemTransactionsListModel()
        {
            //AvailablePublishedOptions = new List<SelectListItem>();
        }
        
        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.List.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.List.WalletID")]
        public string WalletID { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.List.AvailableBalanceFrom")]
        public string AvailableBalanceFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.List.AvailableBalanceTo")]
        public string AvailableBalanceTo { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.List.RedeemWalletBalanceFrom")]
        public string RedeemWalletBalanceFrom { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.RedeemTransactions.List.RedeemWalletBalanceTo")]
        public string RedeemWalletBalanceTo { get; set; }
    }
}