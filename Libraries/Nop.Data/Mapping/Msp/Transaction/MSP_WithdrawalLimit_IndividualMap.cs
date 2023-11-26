using Nop.Core.Domain.Msp.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_WithdrawalLimit_IndividualMap : NopEntityTypeConfiguration<MSP_WithdrawalLimit_Individual> //Atiqah 20181121 MDT-3
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_WithdrawalLimit_IndividualMap()
        {
            this.ToTable("MSP_WithdrawalLimit_Individual");
            this.HasKey(p => p.Id);

            this.Property(m => m.WithdrewAmtInd).HasPrecision(26, 8);
            this.Property(m => m.WithdrewAmtVIP).HasPrecision(26, 8);
            this.Property(m => m.WithdrawalRefundInd).HasPrecision(26, 8);
            this.Property(m => m.WithdrawalRefundVIP).HasPrecision(26, 8);
            this.Property(m => m.WithdrawalPendingInd).HasPrecision(26, 8);
            this.Property(m => m.WithdrawalPendingVIP).HasPrecision(26, 8);
            this.Property(m => m.WithdrawalLimit).HasPrecision(26, 8);
            this.Property(m => m.WithdrawalLimitVIP).HasPrecision(26, 8);
        }
	}
}
