using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Wallet
{
    public partial class MSP_WT_Bank : BaseEntity
    {
        public MSP_WT_Bank()
        {
        }

        /// <summary>
        /// Gets or sets the WalletID
        /// </summary>
        public int WalletID { get; set; }

        /// <summary>
        /// Gets or sets the BatchID
        /// </summary>
        public Guid BatchID { get; set; }

        /// <summary>
        /// Gets or sets the Reference ID
        /// </summary>
        public int RefID { get; set; }

        /// <summary>
        /// Gets or sets a RefType
        /// </summary
        private MSP_WT_Bank_RefType _RefTypeEnum;
        public virtual MSP_WT_Bank_RefType RefTypeEnum
        {
            get { return _RefTypeEnum; }
            set { _RefTypeEnum = value; }
        }
        public string RefType
        {
            get { return _RefTypeEnum.ToValue<MSP_WT_Bank_RefType>(); }
            set { _RefTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_WT_Bank_RefType>(value); }
        }

        /// <summary>
        /// Gets or sets a Amount Type
        /// </summary
        private MSP_WT_Bank_AmountType _AmountTypeEnum;
        public virtual MSP_WT_Bank_AmountType AmountTypeEnum
        {
            get { return _AmountTypeEnum; }
            set { _AmountTypeEnum = value; }
        }
        public string AmountType
        {
            get { return _AmountTypeEnum.ToValue<MSP_WT_Bank_AmountType>(); }
            set { _AmountTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_WT_Bank_AmountType>(value); }
        }

        /// <summary>
        /// Gets or sets the Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the Amount Encryption
        /// </summary>
        public string Amount_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Balance
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the Balance Encryption
        /// </summary>
        public string Balance_Enc { get; set; }

        /// <summary>
        /// Gets or sets the Remark Chinese
        /// </summary>
        public decimal Remark_Cn { get; set; }

        /// <summary>
        /// Gets or sets the Remark English
        /// </summary>
        public decimal Remark_En { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
