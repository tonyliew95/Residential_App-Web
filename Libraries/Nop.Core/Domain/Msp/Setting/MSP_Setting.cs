using System;
using System.Collections.Generic;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Enumeration;

namespace Nop.Core.Domain.Msp.Setting
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class MSP_Setting : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Setting()
        {
           
        }

        /// <summary>
        /// Gets or sets the Setting Key
        /// </summary>
        public string SettingKey { get; set; }

        /// <summary>
        /// Gets or sets the Setting Value
        /// </summary>
        public string SettingValue { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        private MSP_Setting_Status _StatusEnum;
        public virtual MSP_Setting_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Setting_Status>(); }
            set { _StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_Setting_Status>(value); }
        }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; } // Tony Liew 20181204 MDT-122



    }
}