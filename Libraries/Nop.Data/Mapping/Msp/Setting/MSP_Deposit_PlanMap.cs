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
    public partial class MSP_Deposit_PlanMap : NopEntityTypeConfiguration<MSP_Deposit_Plan>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_PlanMap()
        {
            this.ToTable("MSP_Deposit_Plan");
            this.HasKey(p => p.Id);
        }
    }
}
