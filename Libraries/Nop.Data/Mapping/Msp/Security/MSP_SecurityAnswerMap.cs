using Nop.Core.Domain.Msp.Security;

namespace Nop.Data.Mapping.Msp.Security
{
    public partial class MSP_SecurityAnswerMap : NopEntityTypeConfiguration<MSP_SecurityAnswer>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_SecurityAnswerMap()
        {
            this.ToTable("MSP_SecurityAnswer");
            this.HasKey(p => p.Id);
            this.Ignore(p => p.SecurityQuestionTypeEnum);
        }
    }
}
