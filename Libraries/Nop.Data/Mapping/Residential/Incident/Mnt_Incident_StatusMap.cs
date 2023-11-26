using Nop.Core.Domain.Residential.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Residential.Incident
{
    public class Mnt_Incident_StatusMap : NopEntityTypeConfiguration<Mnt_Incident_Status> //wailiang 20190319 RDT-127
    {
        public Mnt_Incident_StatusMap()
        {
            this.ToTable("Mnt_Incident_Status");
            
            this.HasKey(p => p.Id);
        }
    }
}
