using Nop.Core.Domain.Residential.Organization;

// Tony Liew 20190315 RDT-65 \/
namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_OrganizationMap : NopEntityTypeConfiguration<Mnt_Organization>
    {
        public Mnt_OrganizationMap()
        {
            this.ToTable("Mnt_Organization");
            this.HasKey(p => p.Id);
        }
    }
}
// Tony Liew 20190315 RDT-65 /\
