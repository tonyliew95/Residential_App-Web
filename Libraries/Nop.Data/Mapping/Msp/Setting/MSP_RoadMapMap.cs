using Nop.Core.Domain.Msp.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Msp.Setting
{
    public partial class MSP_RoadMapMap : NopEntityTypeConfiguration<MSP_RoadMap>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_RoadMapMap() //Tony Liew 20181221 MDT-148
        {
            //wailiang 20190109 Mapping Length Validation \/
            this.ToTable("MSP_RoadMap");
            this.Property(u => u.Title_EN).HasMaxLength(50);
            this.Property(u => u.Title_CN).HasMaxLength(50);
            this.Property(u => u.Content_EN).HasMaxLength(300);
            this.Property(u => u.Content_CN).HasMaxLength(300);
            this.HasKey(p => p.Id);
            //wailiang 20190109 Mapping Length Validation /\
        }
    }
}
