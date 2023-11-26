using Nop.Core.Domain.Residential.Announcement;


namespace Nop.Data.Mapping.Residential.Announcement
{
    public class Mnt_Announcement_AttachmentMap : NopEntityTypeConfiguration<Mnt_Announcement_Attachment>
    {
        public Mnt_Announcement_AttachmentMap()
        {
            this.ToTable("Mnt_Announcement_Attachment");
            this.HasKey(p => p.Id);
        }
    }
}
