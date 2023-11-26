using Nop.Core.Domain.Residential.User;

//Tony Liew 20190403 RDT-175 \/
namespace Nop.Data.Mapping.Residential.User
{
    public class Mnt_FamilyTenantMap : NopEntityTypeConfiguration<Mnt_FamilyTenant>
    {
        public Mnt_FamilyTenantMap()
        {
            this.ToTable("Mnt_FamilyTenant");
            this.HasKey(p => p.Id);
        }
    }
}
//Tony Liew 20190403 RDT-175 /\