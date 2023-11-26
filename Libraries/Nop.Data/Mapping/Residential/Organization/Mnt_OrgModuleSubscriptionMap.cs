using Nop.Core.Domain.Residential.Organization;

// WKK 20190405 RDT-164 [API] Profile - Detail
namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_OrgModuleSubscriptionMap : NopEntityTypeConfiguration<Mnt_OrgModuleSubscription>
    {
        public Mnt_OrgModuleSubscriptionMap()
        {
            this.ToTable("Mnt_OrgModuleSubscription");
            this.HasKey(p => p.Id);
        }
    }
}

