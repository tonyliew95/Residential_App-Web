using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_WT_ProfitMap : NopEntityTypeConfiguration<MSP_WT_Profit>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WT_ProfitMap()
        {
            this.ToTable("MSP_WT_Profit");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.AmountTypeEnum);
            this.Ignore(p => p.RefTypeEnum);

        }
    }
}
