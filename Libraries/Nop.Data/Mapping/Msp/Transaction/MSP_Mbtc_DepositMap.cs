using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_Mbtc_DepositMap : NopEntityTypeConfiguration<MSP_Mbtc_Deposit>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Mbtc_DepositMap()
        {
            this.ToTable("MSP_Mbtc_Deposit");
            this.HasKey(p => p.Id);
            
            this.Ignore(p => p.StatusEnum);
            this.Ignore(p => p.StatusDescription); //wailiang 20190116 MDT-193
			this.Property(m => m.MbtcAmt).HasPrecision(26, 8); //wailiang 20181023
        }
    }
}
