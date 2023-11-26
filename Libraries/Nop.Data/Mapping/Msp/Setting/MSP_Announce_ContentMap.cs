using Nop.Core.Domain.Msp.Setting;

namespace Nop.Data.Mapping.Msp.Setting
{
    public partial class MSP_Announce_ContentMap : NopEntityTypeConfiguration<MSP_Announce_Content>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Announce_ContentMap()
        {
            //wailiang 20190109 Mapping Length Validation \/
            this.ToTable("MSP_Announce_Content");
            this.Property(u => u.ContentUrl).HasMaxLength(200);
            this.Property(u => u.ContentName).HasMaxLength(200);
            this.Property(u => u.ContentTitle).HasMaxLength(200);
            this.Property(u => u.ContentTitle_CN).HasMaxLength(200);
            this.HasKey(p => p.Id);
            //wailiang 20190109 Mapping Length Validation /\
        }
    }
}
