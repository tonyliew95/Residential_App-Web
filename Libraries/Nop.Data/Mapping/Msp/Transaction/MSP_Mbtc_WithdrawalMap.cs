using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_Mbtc_WithdrawalMap : NopEntityTypeConfiguration<MSP_Mbtc_Withdrawal>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Mbtc_WithdrawalMap()
        {
            this.ToTable("MSP_Mbtc_Withdrawal");
            this.HasKey(p => p.Id);
            
            this.Ignore(p => p.StatusEnum);
			this.Property(m => m.WithdrawAmt).HasPrecision(26, 8); //wailiang 20181023
            this.Property(m => m.NetWithdrawAmt_5Decimal).HasPrecision(24, 5);
			this.Property(m => m.NetWithdrawAmt).HasPrecision(26, 8); //wailiang 20181023
            this.Property(m => m.TxFee).HasPrecision(26, 8); //wailiang 20181023
            this.Property(m => m.BlockChainTxFees).HasPrecision(24, 5);
            this.Property(m => m.TruncateProfit).HasPrecision(26, 8); //WilliamHo 20181024
        }
	}
}
