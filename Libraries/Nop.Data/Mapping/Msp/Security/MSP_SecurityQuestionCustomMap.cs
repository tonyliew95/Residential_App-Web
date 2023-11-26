using Nop.Core.Domain.Msp.Security;

namespace Nop.Data.Mapping.Msp.Security
{
    public partial class MSP_SecurityQuestionCustomMap : NopEntityTypeConfiguration<MSP_SecurityQuestionCustom>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_SecurityQuestionCustomMap()
        {
            this.ToTable("MSP_SecurityQuestionCustom");
            this.HasKey(p => p.Id);
        }
    }
}
