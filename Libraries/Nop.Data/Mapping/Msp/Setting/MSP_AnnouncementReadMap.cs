using Nop.Core.Domain.Msp.Setting;

namespace Nop.Data.Mapping.Msp.Setting
{
    public partial class MSP_AnnouncementReadMap : NopEntityTypeConfiguration<MSP_AnnouncementRead>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_AnnouncementReadMap()
        {
            this.ToTable("MSP_AnnouncementRead");
            this.HasKey(p => p.Id);
        }
    }
}
