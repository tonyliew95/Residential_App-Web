using Nop.Core.Domain.Residential.Announcement;

namespace Nop.Data.Mapping.Residential.Announcement
{
    public class Mnt_AnnouncementMap : NopEntityTypeConfiguration<Mnt_Announcement>
    {
        public Mnt_AnnouncementMap()
        {
            this.ToTable("Mnt_Announcement");
            this.HasKey(p => p.Id);
        }
    }
}
