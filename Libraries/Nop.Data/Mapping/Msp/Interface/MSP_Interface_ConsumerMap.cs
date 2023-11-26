using Nop.Core.Domain.Msp.Interface;

namespace Nop.Data.Mapping.Msp.Interface
{
    public partial class MSP_Interface_ConsumerMap : NopEntityTypeConfiguration<MSP_Interface_Consumer>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Interface_ConsumerMap()
        {
            this.ToTable("MSP_Interface_Consumer");
            this.HasKey(p => p.Id);
        }
    }
}

