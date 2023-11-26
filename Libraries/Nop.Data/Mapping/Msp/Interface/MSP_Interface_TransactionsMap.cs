using Nop.Core.Domain.Msp.Interface;

namespace Nop.Data.Mapping.Msp.Interface
{
    public partial class MSP_Interface_TransactionsMap : NopEntityTypeConfiguration<MSP_Interface_Transactions>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Interface_TransactionsMap()
        {
            this.ToTable("MSP_Interface_Transactions");
            this.HasKey(p => p.Id);
            this.Property(m => m.MerchantAmount).HasPrecision(26, 8); //wailiang 20181023
            this.Property(m => m.OrderAmount).HasPrecision(26, 8); //wailiang 20181023
        }
    }
}
