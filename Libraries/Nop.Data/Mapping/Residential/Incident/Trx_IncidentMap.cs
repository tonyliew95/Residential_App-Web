using Nop.Core.Domain.Residential.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Residential.Incident
{
    public class Trx_IncidentMap : NopEntityTypeConfiguration<Trx_Incident>
    {
        public Trx_IncidentMap()
        {
            this.ToTable("Trx_Incident");

            //this.HasRequired(p => p.Incidents)
            //    .WithMany()
            //    .HasForeignKey(p => p.Id);

            this.HasKey(p => p.Id);
        }
    }
}
