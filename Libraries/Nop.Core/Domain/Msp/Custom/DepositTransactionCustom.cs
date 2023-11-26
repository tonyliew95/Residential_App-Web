using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Custom
{
    public class DepositTransactionCustom
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
        /// Gets or sets the Total Deposit Amount
        /// </summary>
        public decimal DepositAmt { get; set; }
        
        //wailiang 20190115 MDT-194 \/

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Status { get; set; }
        //wailiang 20190115 MDT-194 /\
        
    }
}
