using Nop.Core.Domain.Msp.Calculation;
using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Calculation
{
    public partial class MSP_Deposit_RuleMap : NopEntityTypeConfiguration<MSP_Deposit_Rule>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Deposit_RuleMap()
        {
            this.ToTable("MSP_Deposit_Rule");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.StatusEnum); 
        }
    }
}
