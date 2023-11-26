using Nop.Core.Domain.Msp.Security;

namespace Nop.Data.Mapping.Msp.Security
{
    public partial class MSP_Security_TokenMap : NopEntityTypeConfiguration<MSP_Security_Token>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Security_TokenMap()
        {
            this.ToTable("MSP_Security_Token");
            this.HasKey(p => p.Id);
            this.Ignore(p => p.TokenTypeEnum);
        }
    }
}
