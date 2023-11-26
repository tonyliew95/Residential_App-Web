using Nop.Core.Domain.Msp.Transaction;

namespace Nop.Data.Mapping.Msp.Transaction
{
    public partial class MSP_ConsumptionMap : NopEntityTypeConfiguration<MSP_Consumption>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_ConsumptionMap()
        {
            this.ToTable("MSP_Consumption");
            this.HasKey(p => p.Id);
            
            this.Ignore(p => p.StatusEnum);
            this.Property(m => m.ConsumptionAmtDistPct).HasPrecision(5, 2);
            this.Property(m => m.ConsumptionAmt).HasPrecision(26, 8); //wailiang 20181023
            this.Property(m => m.GuaranteedAmtDistPct).HasPrecision(5, 2);
            this.Property(m => m.GuaranteedAmt).HasPrecision(26, 8); //wailiang 20181023
            this.Property(m => m.MerchantReferralAmt).HasPrecision(26, 8); //wailiang 20181023
            this.Property(m => m.TruncateProfit).HasPrecision(26, 8); //wailiang 20181023
        }
    }
}
