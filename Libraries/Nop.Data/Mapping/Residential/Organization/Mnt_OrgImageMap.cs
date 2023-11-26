using Nop.Core.Domain.Residential.Organization;

// WKK 20190403 RDT-164 [API] Profile - Detail
namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_OrgImageMap : NopEntityTypeConfiguration<Mnt_OrgImage>
    {
        public Mnt_OrgImageMap()
        {
            this.ToTable("Mnt_OrgImage");
            this.HasKey(p => p.Id);
        }
    }
}

