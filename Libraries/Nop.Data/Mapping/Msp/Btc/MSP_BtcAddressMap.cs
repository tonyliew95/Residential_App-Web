using Nop.Core.Domain.Msp.Btc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Btc
{
    public partial class MSP_BtcAddressMap : NopEntityTypeConfiguration<MSP_BtcAddress>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_BtcAddressMap()
        {
            this.ToTable("MSP_BtcAddress");
            this.HasKey(p => p.Id);
        }
    }
}
