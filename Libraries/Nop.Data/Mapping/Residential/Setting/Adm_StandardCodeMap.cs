using Nop.Core.Domain.Residential.Setting;

namespace Nop.Data.Mapping.Residential.Setting
{
    public class Adm_StandardCodeMap : NopEntityTypeConfiguration<Adm_StandardCode>
    {
        public Adm_StandardCodeMap()
        {
            this.ToTable("Adm_StandardCode");
            this.HasKey(p => p.Id);
        }
    }
}
