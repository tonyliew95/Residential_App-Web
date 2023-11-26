using Nop.Core.Domain.Residential.Mobile;

namespace Nop.Data.Mapping.Residential.Mobile
{
    public class Mnt_MobileSessionMap : NopEntityTypeConfiguration<Mnt_MobileSession>
    {
        public Mnt_MobileSessionMap()
        {
            this.ToTable("Mnt_MobileSession");
            this.HasKey(p => p.Id);
        }
    }
}
