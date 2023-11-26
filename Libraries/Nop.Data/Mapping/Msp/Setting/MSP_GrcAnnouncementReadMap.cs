using Nop.Core.Domain.Msp.Setting;

namespace Nop.Data.Mapping.Msp.Setting
{
    public partial class MSP_GrcAnnouncementReadMap : NopEntityTypeConfiguration<MSP_GrcAnnouncementRead> //wailiang 20181227 MDT-176
    {
        public MSP_GrcAnnouncementReadMap()
        {
            this.ToTable("MSP_GrcAnnouncementRead");
            this.HasKey(p => p.Id);
        }
    }
}
