using Nop.Core.Domain.Residential.Incident;


namespace Nop.Data.Mapping.Residential.Incident
{
    public class Trx_Incident_FileMap : NopEntityTypeConfiguration<Trx_Incident_File>
    {
        public Trx_Incident_FileMap()
        {
            this.ToTable("Trx_Incident_File");
            //this.HasRequired(p => p.Incidents)
            //   .WithRequiredDependent()
            //   .Map(m => m.MapKey().ToTable("Trx_IncidentMap"));
            this.HasKey(p => p.Id);
        }
    }
}
