using Nop.Core.Domain.Msp.Security;

namespace Nop.Data.Mapping.Msp.Security
{
    public partial class MSP_Change_Email_TokenMap : NopEntityTypeConfiguration<MSP_Change_Email_Token>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Change_Email_TokenMap()
        {
            this.ToTable("MSP_Change_Email_Token");
            this.HasKey(p => p.Id);
        }
    }
}
