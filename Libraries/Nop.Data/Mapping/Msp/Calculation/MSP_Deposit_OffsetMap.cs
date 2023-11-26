using Nop.Core.Domain.Msp.Calculation;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Calculation
{
    public partial class MSP_Deposit_OffsetMap : NopEntityTypeConfiguration<MSP_Deposit_Offset>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_OffsetMap()
        {
            this.ToTable("MSP_Deposit_Offset");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.StatusEnum); 
        }
    }
}
