using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_WithdrawalLimit_GlobalMap : NopEntityTypeConfiguration<MSP_WithdrawalLimit_Global> //Atiqah 20181121 MDT-3
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WithdrawalLimit_GlobalMap()
        {
            this.ToTable("MSP_WithdrawalLimit_Global");
            this.HasKey(p => p.Id);
            
			this.Property(m => m.WithdrewAmt).HasPrecision(26, 8); 
            this.Property(m => m.WithdrawalRefund).HasPrecision(26, 8);
			this.Property(m => m.WithdrawalPending).HasPrecision(26, 8); 
            this.Property(m => m.WithdrawalLimit).HasPrecision(26, 8);
        }
	}
}
