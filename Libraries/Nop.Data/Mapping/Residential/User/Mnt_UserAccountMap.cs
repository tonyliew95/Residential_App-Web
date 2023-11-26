using Nop.Core.Domain.Residential.User;

namespace Nop.Data.Mapping.Residential.User
{
    public class Mnt_UserAccountMap : NopEntityTypeConfiguration<Mnt_UserAccount>
    {
        public Mnt_UserAccountMap()
        {
            this.ToTable("Mnt_UserAccount");
            this.HasKey(p => p.Id);
        }
    }
}
