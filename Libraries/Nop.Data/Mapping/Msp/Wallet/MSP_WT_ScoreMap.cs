using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_WT_ScoreMap : NopEntityTypeConfiguration<MSP_WT_Score>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WT_ScoreMap()
        {
            this.ToTable("MSP_WT_Score");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.RefTypeEnum);
            this.Ignore(p => p.AmountType); 
        }
    }
}
