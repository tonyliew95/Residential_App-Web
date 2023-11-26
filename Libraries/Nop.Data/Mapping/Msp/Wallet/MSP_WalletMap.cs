using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_WalletMap : NopEntityTypeConfiguration<MSP_Wallet>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WalletMap()
        {
            this.ToTable("MSP_Wallet");
            this.HasKey(p => p.Id);
        }
    }
}
