using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Msp.Setting;

namespace Nop.Data.Mapping.Msp.Setting
{
    public partial class MSP_Consumption_PlatformMap : NopEntityTypeConfiguration<MSP_Consumption_Platform>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_Consumption_PlatformMap()
        {
            this.ToTable("MSP_Consumption_Platform");
            this.HasKey(p => p.Id);
        }
    }
}
