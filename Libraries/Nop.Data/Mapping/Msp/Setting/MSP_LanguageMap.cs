using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Setting;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_LanguageMap : NopEntityTypeConfiguration<MSP_Language>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_LanguageMap() //wailiang 20181122 MSP-19
        {
            this.ToTable("MSP_Language");
            this.HasKey(p => p.Id);
        }
    }
}
