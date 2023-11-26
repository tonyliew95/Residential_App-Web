using Nop.Core.Domain.Residential.User;

namespace Nop.Data.Mapping.Residential.User
{
    public class Mnt_UserProfileMap : NopEntityTypeConfiguration<Mnt_UserProfile>
    {
        public Mnt_UserProfileMap()
        {
            this.ToTable("Mnt_UserProfile");
            this.HasKey(p => p.Id);
        }
    }
}
