using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_SettlementMap : NopEntityTypeConfiguration<MSP_Settlement>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_SettlementMap()
        {
            this.ToTable("MSP_Settlement");
            this.HasKey(p => p.Id);
            
            this.Ignore(p => p.StatusEnum);
            this.Property(m => m.SettleAmt).HasPrecision(26, 8);
            this.Property(m => m.MbtcBal).HasPrecision(26, 8);
            this.Property(m => m.MbtcAfterSettle).HasPrecision(26, 8);
            this.Property(m => m.MbtcFloatBal).HasPrecision(26, 8);
            this.Property(m => m.MbtcFloatAfterSettle).HasPrecision(26, 8);
        }
    }
}
