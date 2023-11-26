using Nop.Core.Domain.Residential.Setting;

namespace Nop.Data.Mapping.Residential.Setting
{
    public class Adm_MsgLocalizationMap : NopEntityTypeConfiguration<Adm_MsgLocalization>
    {
        public Adm_MsgLocalizationMap()
        {
            this.ToTable("Adm_MsgLocalization");
            this.HasKey(p => p.Id);
        }
    }
}
