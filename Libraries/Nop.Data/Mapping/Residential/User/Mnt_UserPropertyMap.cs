using Nop.Core.Domain.Residential.User;

namespace Nop.Data.Mapping.Residential.User
{
    public class Mnt_UserPropertyMap : NopEntityTypeConfiguration<Mnt_UserProperty>
    {
        public Mnt_UserPropertyMap()
        {
            this.ToTable("Mnt_UserProperty");
            this.HasKey(p => p.Id);
        }
    }
}
