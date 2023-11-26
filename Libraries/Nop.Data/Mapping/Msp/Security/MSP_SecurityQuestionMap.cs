using Nop.Core.Domain.Msp.Security;

namespace Nop.Data.Mapping.Msp.Security
{
    public partial class MSP_SecurityQuestionMap : NopEntityTypeConfiguration<MSP_SecurityQuestion>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_SecurityQuestionMap()
        {
            this.ToTable("MSP_SecurityQuestion");
            this.HasKey(p => p.Id);

            this.Ignore(p => p.StatusEnum); 
        }
    }
}
