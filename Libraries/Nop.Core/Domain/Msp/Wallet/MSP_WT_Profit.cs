using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Wallet
{
    public partial class MSP_WT_Profit : BaseEntity
    {
        public MSP_WT_Profit()
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

        private MSP_WT_Profit_RefType _RefTypeEnum;
        public MSP_WT_Profit_RefType RefTypeEnum { get; set; }
        public string RefType
        {
            get { return _RefTypeEnum.ToValue<MSP_WT_Profit_RefType>(); }
            set { _RefTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_WT_Profit_RefType>(value); }
        }

        private MSP_WT_Profit_AmountType _AmountTypeEnum;
        public MSP_WT_Profit_AmountType AmountTypeEnum { get; set; }
        public string AmountType
        {
            get { return _AmountTypeEnum.ToValue<MSP_WT_Profit_AmountType>(); }
            set { _AmountTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_WT_Profit_AmountType>(value); }
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

        // 20181220 Chew MDT-157
        ///// <summary>
        ///// Gets or sets the Remark Chinese
        ///// </summary>
        //public string Remark_Cn { get; set; }

        /// <summary>
        /// Gets or sets the Remark English
        /// </summary>
        public string Remark_En { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
