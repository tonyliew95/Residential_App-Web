using Nop.Core.Domain.Residential.User;

namespace Nop.Data.Mapping.Residential.User
{
    public class Mnt_UserEmailMap : NopEntityTypeConfiguration<Mnt_UserEmail>
    {
        public Mnt_UserEmailMap()
        {
            this.ToTable("Mnt_UserEmail");
            this.HasKey(p => p.Id);
        }
    }
}
