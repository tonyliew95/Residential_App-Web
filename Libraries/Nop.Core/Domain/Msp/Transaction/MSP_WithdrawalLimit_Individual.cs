using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Transaction
{
    /// <summary>
    /// Represents a MSP_WithdawalLimit_Individual
    /// </summary>
    public partial class MSP_WithdrawalLimit_Individual : BaseEntity //Atiqah 20181121 MDT-3
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WithdrawalLimit_Individual()
        {
           
        }

        /// <summary>
        /// Gets or sets the Withdrawal Date
        /// </summary>
        public DateTime WithdrawalDate { get; set; }

        /// <summary>
        /// Gets or sets the Cutsomer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Withdrew Amount
        /// </summary>
        public decimal WithdrewAmtInd { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Refund
        /// </summary>
        public decimal WithdrewAmtVIP { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Pending
        /// </summary>
        public decimal WithdrawalRefundInd { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Limit
        /// </summary>
        public decimal WithdrawalRefundVIP { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Limit VIP
        /// </summary>
        public decimal WithdrawalPendingInd { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Limit
        /// </summary>
        public decimal WithdrawalPendingVIP { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Limit VIP
        /// </summary>
        public decimal WithdrawalLimit { get; set; }

        /// <summary>
        /// Gets or sets the Withdrawal Limit
        /// </summary>
        public decimal WithdrawalLimitVIP { get; set; }

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
