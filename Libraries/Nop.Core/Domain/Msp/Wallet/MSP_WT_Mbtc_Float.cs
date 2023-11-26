using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Wallet
{
    public partial class MSP_WT_Mbtc_Float : BaseEntity
    {
        public MSP_WT_Mbtc_Float()
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
        /// Gets or sets the Reference Type
        /// </summary>
        private MSP_WT_Mbtc_Float_RefType _RefTypeEnum;
        public MSP_WT_Mbtc_Float_RefType RefTypeEnum { get; set; }
        public string RefType
        {
            get { return _RefTypeEnum.ToValue<MSP_WT_Mbtc_Float_RefType>(); }
            set { _RefTypeEnum = EnumExtendMethod.GetEnumFromValue<MSP_WT_Mbtc_Float_RefType>(value); }
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
