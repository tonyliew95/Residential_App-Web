using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Custom
{
    public class WithdrawalTransactionCustom
    {
        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Date 
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the Contributed By
        /// </summary>
        public int TxnNo { get; set; }

        /// <summary>
        /// Gets or sets the Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the TransactionFees
        /// </summary>
        public decimal TransactionFees { get; set; }

        /// <summary>
        /// Gets or sets the Net Amount
        /// </summary>
        public decimal NetAmount { get; set; }

        //wailiang 20190116 MDT-195 \/
        /// <summary>
        /// Gets or sets the Blockchain Tx Id
        /// </summary>
        public string BlockChainTxId { get; set; }

        /// <summary>
        /// Gets or sets the Status 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Refund Status - RefundProcessStatus
        /// </summary>
        public string RefundStatus { get; set; }

        /// <summary>
        /// Gets or sets the Remark - Error Messsage
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Address
        /// </summary>
        public string WithdrawalAddress { get; set; }

        //wailiang 20190116 MDT-195 /\
    }
}
