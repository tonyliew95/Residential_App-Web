using Nop.Core.Domain.Msp.Setting;

//Tony Liew 20181227 MDT-173 \/
namespace Nop.Data.Mapping.Msp.Setting
{
    public partial class MSP_GrcAnnouncementMap : NopEntityTypeConfiguration<MSP_GrcAnnouncement>
    {
        public MSP_GrcAnnouncementMap()
        {
            //wailiang 20190109 Mapping Length Validation \/
            this.ToTable("MSP_GrcAnnouncement");
            this.Property(u => u.Title_EN).HasMaxLength(50);
            this.Property(u => u.ShortDescription_EN).HasMaxLength(100);
            this.Property(u => u.Content2_EN).HasMaxLength(300);
            this.Property(u => u.Title_CN).HasMaxLength(50);
            this.Property(u => u.ShortDescription_CN).HasMaxLength(100);
            this.Property(u => u.Content2_CN).HasMaxLength(300);
            this.HasKey(p => p.Id);
            //wailiang 20190109 Mapping Length Validation /\
        }
    }
}
//Tony Liew 20181227 MDT-173 /\
