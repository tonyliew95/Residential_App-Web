using Nop.Core.Domain.Residential.Organization;

// WKK 20190322 RDT-163 [API] Login - profile dto
namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_Org_BlockMap : NopEntityTypeConfiguration<Mnt_Org_Block>
    {
        public Mnt_Org_BlockMap()
        {
            this.ToTable("Mnt_Org_Block");
            this.HasKey(p => p.Id);
        }
    }
}
