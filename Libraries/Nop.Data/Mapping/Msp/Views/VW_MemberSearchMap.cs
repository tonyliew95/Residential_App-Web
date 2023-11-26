using Nop.Core.Domain.Msp.Views;

namespace Nop.Data.Mapping.Msp.Views
{
    public partial class VW_MemberSearchMap : NopEntityTypeConfiguration<VW_MemberSearch>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public VW_MemberSearchMap()
        {
            this.ToTable("vw_MemberInformation");
            this.HasKey(p => p.MemberId);
            this.Property(m => m.Mbtc).HasPrecision(26, 8);
            this.Property(m => m.Deposit).HasPrecision(26, 8);
            this.Property(m => m.Consumption).HasPrecision(26, 8);
            this.Property(m => m.Score_Y).HasPrecision(26, 8);
            this.Property(m => m.ScorePct_Y).HasPrecision(26, 8);
            this.Property(m => m.Score_Z).HasPrecision(26, 8);
            this.Property(m => m.ScorePct_Z).HasPrecision(26, 8);
            this.Property(m => m.Mbtc_Withdrawal_Total).HasPrecision(26, 8);
            this.Property(m => m.Mbtc_Deposit_Total).HasPrecision(26, 8);
            this.Property(m => m.Deposit_Total).HasPrecision(26, 8);
            this.Property(m => m.Profit_Total).HasPrecision(26, 8);
        }
    }
}
