using Nop.Core.Domain.Residential.Organization;

// WKK 20190322 RDT-163 [API] Login - profile dto
namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_Org_LevelMap : NopEntityTypeConfiguration<Mnt_Org_Level>
    {
        public Mnt_Org_LevelMap()
        {
            this.ToTable("Mnt_Org_Level");
            this.HasKey(p => p.Id);
        }
    }
}
