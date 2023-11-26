using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Transaction
{
    public class TxnTopUpListViewModel : BaseNopModel //MSP-46 Backend Function: Transaction Listing > Top Up
    {
        public TxnTopUpListViewModel()
        {
            this.TxnTopUpList = new List<TxnTopUp>();
            //wailiang 20181002 MSP-190 \/
            //this.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            //this.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59);
            //wailiang 20181002 MSP-190 /\
            Status = new List<SelectListItem>();
        }

        //filter
        [NopResourceDisplayName("Admin.TransactionList.TopUp.Fields.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.TopUp.Fields.DateFrom")]
        //[UIHint("DateNullable")]
        [UIHint("DateFrom")]  //wailiang 20181002 MSP-190
        public DateTime? DateFrom { get; set; }
        //public bool DateFromEnabled { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.TopUp.Fields.DateTo")]
        //[UIHint("DateNullable")]
        [UIHint("DateTo")]//wailiang 20181002 MSP-190
        public DateTime? DateTo { get; set; }
        //public bool DateToEnabled { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.TopUp.Fields.TxnId")]
        [UIHint("Int32Nullable")]
        public int? TxnId { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.TopUp.Fields.TopUpAmt")]
        [UIHint("DecimalNullable")]
        public decimal? TopUpAmt { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.TopUp.Fields.TopUpMbtcAdd")]
        public string TopUpMbtcAdd { get; set; }

        //wailiang 20190116 MDT-193 \/
        [NopResourceDisplayName("Admin.TransactionList.TopUp.Fields.BlockchainTxId")]
        public string BlockchainTxId { get; set; }

        [NopResourceDisplayName("Admin.TransactionList.TopUp.Fields.Status")]
        //public string Status { get; set; }
        public string StatusValue { get; set; }
        public IList<SelectListItem> Status { get; set; }
        //wailiang 20190116 MDT-193 /\

        public class TxnTopUp
        {   
            public string Username { get; set; }

            public DateTime Date { get; set; }

            public int? TxId { get; set; }

            public decimal TopUpAmt { get; set; }

            public string TopUpWalletAdd { get; set; }

            //wailiang 20190116 MDT-193 \/
            public string BlockchainTxId { get; set; }

            public string Status { get; set; }
            //wailiang 20190116 MDT-193 /\

            //public int Id { get; set; }
        }

        //response
        public IList<TxnTopUp> TxnTopUpList { get; set; }
    }
}
