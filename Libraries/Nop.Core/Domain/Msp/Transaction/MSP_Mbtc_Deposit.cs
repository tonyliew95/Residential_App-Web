using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Transaction
{
    /// <summary>
    /// Represents a MSP_Deposit_Mbtc
    /// </summary>
    public partial class MSP_Mbtc_Deposit : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Mbtc_Deposit()
        {
            IsSync = false;
        }

        ///// <summary>
        ///// Gets or sets the Batch Id
        ///// </summary>
        //public Guid BatchID { get; set; }

        /// <summary>
        /// Gets or sets the Customer Id
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the Wallet ID
        /// </summary>
        public int WalletID { get; set; }

        /// <summary>
        /// Gets or sets the MbtcAmt
        /// </summary>
        public decimal MbtcAmt { get; set; }

        /// <summary>
        /// Gets or sets the MbtcAmt Encryption
        /// </summary>
        public string MbtcAmt_Enc { get; set; }

        /// Gets or sets a Remark
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets a Status
        /// </summary
        private MSP_Mbtc_Deposit_Status _StatusEnum;
        public virtual MSP_Mbtc_Deposit_Status StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }
        public string StatusDescription //wailiang 20190116 MDT-193
        {
            get { return _StatusEnum.ToDescription<MSP_Mbtc_Deposit_Status>(); }
            set
            {
                StatusEnum = EnumExtendMethod.GetEnumFromDescription<MSP_Mbtc_Deposit_Status>(value);
            }
        }
        public string Status
        {
            get { return _StatusEnum.ToValue<MSP_Mbtc_Deposit_Status>(); }
            set
            {
                StatusEnum = EnumExtendMethod.GetEnumFromValue<MSP_Mbtc_Deposit_Status>(value);
            }
        }

        /// <summary>
        /// Gets or sets the date and time of created 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets created by Id 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date and time of updated 
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets updated by Id 
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the walletaddress
        /// </summary>
        public string WalletAddress { get; set; }

        #region Added Column
        /// <summary>
        /// Gets or sets the Transaction ID
        /// </summary>
        public int? TxID { get; set; }

        /// <summary>
        /// Gets or sets the Address ID
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        /// Gets or sets the Block Chain Transaction ID
        /// </summary>
        public string BlockChainTxId { get; set; }

        /// <summary>
        /// Gets or sets the ReceiveTimeUTC
        /// </summary>
        public DateTime? ReceiveTimeUTC { get; set; }

        /// <summary>
        /// Gets or sets the Conformations
        /// </summary>
        public int? Confirmations { get; set; }
        #endregion

        /// <summary>
        /// Gets or sets sync audit db
        /// </summary>
        public bool IsSync { get; set; }
        


    }
}
