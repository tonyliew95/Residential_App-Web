using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_WT_Mbtc_FloatMap : NopEntityTypeConfiguration<MSP_WT_Mbtc_Float>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WT_Mbtc_FloatMap()
        {
            this.ToTable("MSP_WT_Mbtc_Float");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.RefTypeEnum); 
        }
    }
}
