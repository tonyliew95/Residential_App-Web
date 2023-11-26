using Nop.Core.Enumeration;
using System;

namespace Nop.Core.Domain.Msp.Security
{
    public partial class MSP_Security_Token : BaseEntity
    {
        public MSP_Security_Token()
        {

        }

        /// <summary>
        /// Gets or sets the Token ID
        /// </summary>
        public Guid TokenID { get; set; }

        /// <summary>
        /// Gets or sets the Token Type
        /// </summary>
        private MSP_Security_Token_TokenType _TokenTypeEnum;
        public virtual MSP_Security_Token_TokenType TokenTypeEnum
        {
            get { return _TokenTypeEnum; }
            set { _TokenTypeEnum = value; }
        }
        public string TokenType
        {
            get { return _TokenTypeEnum.ToValue<MSP_Security_Token_TokenType>(); }
            set { _TokenTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_Security_Token_TokenType>(value); }
        }

        /// <summary>
        /// Gets or sets the Customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the SecurityCheck
        /// </summary>
        public bool SecurityChecked { get; set; }

        /// <summary>
        /// Gets or sets the IsValid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the ExpiredOnUtc
        /// </summary>
        public DateTime? ExpiredOnUtc { get; set; }
    }
}
