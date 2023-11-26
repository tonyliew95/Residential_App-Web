using Nop.Core.Domain.Residential.Setting;

namespace Nop.Data.Mapping.Residential.Setting
{
    public class Adm_TimeZoneMap : NopEntityTypeConfiguration<Adm_TimeZone>
    {
        public Adm_TimeZoneMap()
        {
            this.ToTable("Adm_TimeZone");
            this.HasKey(p => p.Id);
        }
    }
}
