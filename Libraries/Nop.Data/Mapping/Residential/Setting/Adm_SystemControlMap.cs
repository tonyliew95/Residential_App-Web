using Nop.Core.Domain.Residential.Setting;

namespace Nop.Data.Mapping.Residential.Setting
{
    public class Adm_SystemControlMap: NopEntityTypeConfiguration<Adm_SystemControl>
    {
        public Adm_SystemControlMap()
        {
            this.ToTable("Adm_SystemControl");
            this.HasKey(p => p.Id);
        }
    }
}
