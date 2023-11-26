using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Transaction
{
    /// <summary>
    /// Represents a MSP_WithdawalLimit_Global
    /// </summary>
    public partial class MSP_WithdrawalLimit_Global : BaseEntity //Atiqah 20181121 MDT-3
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WithdrawalLimit_Global()
        {
           
        }

        /// <summary>
        /// Gets or sets the Withdrawal Date
        /// </summary>
        public DateTime WithdrawalDate { get; set; }

        /// <summary>
        /// Gets or sets the Withdrew Amount
        /// </summary>
        public decimal WithdrewAmt { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Refund
        /// </summary>
        public decimal WithdrawalRefund { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Pending
        /// </summary>
        public decimal WithdrawalPending { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Limit
        /// </summary>
        public decimal WithdrawalLimit { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the Updated datetime on UTC
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
