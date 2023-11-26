using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;

namespace Nop.Core.Domain.Msp.Member
{
    /// <summary>
    /// Represents a Session
    /// </summary>
    public partial class MSP_Session : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Session()
        {
           
        }

        /// <summary>
        /// Gets or sets the Session ID
        /// </summary>
        public Guid SessionID { get; set; }

        /// <summary>
        /// Gets or sets the Token Id
        /// </summary>
        public string TokenId { get; set; } //WilliamHo 20181030

        /// <summary>
        /// Gets or sets the customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the IsTranSuccess
        /// </summary>
        public bool IsLogout { get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public DateTime LoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of logout 
        /// </summary>
        public DateTime? LogoutDateUtc { get; set; }

    }
}