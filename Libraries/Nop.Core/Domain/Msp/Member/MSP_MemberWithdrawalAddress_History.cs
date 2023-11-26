using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Enumeration;

namespace Nop.Core.Domain.Msp.Member
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class MSP_MemberWithdrawalAddress_History : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_MemberWithdrawalAddress_History()
        {

        }

        /// <summary>
        /// Gets or sets the CustomerID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets WalletAddress
        /// </summary>
        public string WalletAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}