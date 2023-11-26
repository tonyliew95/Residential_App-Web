using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Enumeration;

namespace Nop.Core.Domain.Msp.Security
{
    /// <summary>
    /// Represents a Session
    /// </summary>
    public partial class MSP_SecurityQuestion : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_SecurityQuestion()
        {
           
        }

        /// <summary>
        /// Gets or sets the Question
        /// </summary>
        public string Question{ get; set; }

        /// <summary>
        /// Gets or sets the Address Type
        /// </summary>
        private MSP_SecurityQuestion_Status _StatusEnum;
        public virtual MSP_SecurityQuestion_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_SecurityQuestion_Status>(); }
            set { _StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_SecurityQuestion_Status>(value); }
        }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public DateTime CreatedOnUTC { get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public int UpdatedBy { get; set; }

    }
}