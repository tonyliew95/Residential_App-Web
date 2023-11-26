using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_WT_BankMap : NopEntityTypeConfiguration<MSP_WT_Bank>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WT_BankMap()
        {
            this.ToTable("MSP_WT_Bank");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.AmountTypeEnum);
            this.Ignore(p => p.RefTypeEnum);

        }
    }
}
