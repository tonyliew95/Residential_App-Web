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
    public partial class MSP_MemberScorePct : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_MemberScorePct()
        {
            
        }

        /// <summary>
        /// Gets or sets the customer GUID
        /// </summary>
        public Guid CustomerID { get; set; }


        /// <summary>
        /// Gets or sets the Score Type
        /// </summary>
        private MSP_MemberScorePct_ScoreType _ScoreTypeEnum; 
        public MSP_MemberScorePct_ScoreType ScoreTypeEnum { get; set; }
        public string ScoreType
        {
            get { return _ScoreTypeEnum.ToValue<MSP_MemberScorePct_ScoreType>(); }
            set { _ScoreTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_MemberScorePct_ScoreType>(value); }
        }

        /// <summary>
        /// Gets or sets the Score Type
        /// </summary>

        /// <summary>
        /// Gets or sets the Setting ID
        /// </summary>
        public int Setting_ID { get; set; }

        /// <summary>
        /// Gets or sets the Score Amount
        /// </summary>
        public decimal ScoreAmt { get; set; }

        /// <summary>
        /// Gets or sets the Score Percentage Y
        /// </summary>
        public decimal ScorePct { get; set; }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

    }
}