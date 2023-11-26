using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_Transaction_FeesMap : NopEntityTypeConfiguration<MSP_Transaction_Fees>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Transaction_FeesMap()
        {
            this.ToTable("MSP_Transaction_Fees");
            this.HasKey(p => p.Id);
            
            this.Ignore(p => p.StatusEnum);
            this.Ignore(p => p.FeesTypeEnum);
            this.Ignore(p => p.TransactionTypeEnum);

        }
    }
}
