using Nop.Core.Domain.Residential.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Residential.Incident
{
    public class Mnt_Incident_CategoryMap : NopEntityTypeConfiguration<Mnt_Incident_Category> //wailiang 20190319 RDT-127
    {
        public Mnt_Incident_CategoryMap()
        {
            this.ToTable("Mnt_Incident_Category");
            
            this.HasKey(p => p.Id);
        }
    }
}
