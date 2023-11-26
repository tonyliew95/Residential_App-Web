using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_DepositMap : NopEntityTypeConfiguration<MSP_Deposit>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_DepositMap()
        {
            this.ToTable("MSP_Deposit");
            this.HasKey(p => p.Id);
            
            this.Ignore(p => p.StatusEnum);
            this.Property(m => m.DepositAmt).HasPrecision(26, 8); //RW 20181022 MSP-368
        }
    }
}
