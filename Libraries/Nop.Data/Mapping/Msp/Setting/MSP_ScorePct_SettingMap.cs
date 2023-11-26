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
    public partial class MSP_ScorePct_SettingMap : NopEntityTypeConfiguration<MSP_ScorePct_Setting>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_ScorePct_SettingMap()
        {
            this.ToTable("MSP_ScorePct_Setting");
            this.HasKey(p => p.Id);
        }
    }
}
