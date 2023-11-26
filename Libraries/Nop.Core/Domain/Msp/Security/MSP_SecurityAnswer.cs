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
    public partial class MSP_SecurityAnswer : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_SecurityAnswer()
        {
           
        }

        /// <summary>
        /// Gets or sets the Session ID
        /// </summary>
        public int SecurityQuestionID{ get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public DateTime CreatedOnUTC { get; set; }

        /// <summary>
        /// Gets or sets the utc date and time of login
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the Security Question Type
        /// </summary>
        private MSP_SecurityAnswer_SecurityQuestionType _SecurityQuestionTypeEnum;

        public virtual MSP_SecurityAnswer_SecurityQuestionType SecurityQuestionTypeEnum
        {
            get { return _SecurityQuestionTypeEnum; }
            set { _SecurityQuestionTypeEnum = value; }
        }

        public string SecurityQuestionType
        {
            get { return _SecurityQuestionTypeEnum.ToValue<MSP_SecurityAnswer_SecurityQuestionType>(); }
            set { _SecurityQuestionTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_SecurityAnswer_SecurityQuestionType>(value); }
        }

    }
}