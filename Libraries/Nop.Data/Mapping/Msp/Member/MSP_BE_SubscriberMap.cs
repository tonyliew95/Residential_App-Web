using Nop.Core.Domain.Msp.Member;

namespace Nop.Data.Mapping.Msp.Member
{
    public partial class MSP_BE_SubscriberMap : NopEntityTypeConfiguration<MSP_BE_Subscriber>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_BE_SubscriberMap()
        {
            this.ToTable("MSP_BE_Subscriber");
            this.HasKey(p => p.Id);
        }
    }
}
