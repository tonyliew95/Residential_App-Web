using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_WT_ConsumptionMap : NopEntityTypeConfiguration<MSP_WT_Consumption>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WT_ConsumptionMap()
        {
            this.ToTable("MSP_WT_Consumption");
            this.HasKey(p => p.Id);
        }
    }
}
