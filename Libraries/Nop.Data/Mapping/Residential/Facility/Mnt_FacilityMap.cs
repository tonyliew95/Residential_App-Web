using Nop.Core.Domain.Residential.Facility;


//Tony Liew 20190417 RDT-202 \/
namespace Nop.Data.Mapping.Residential.Facility
{
    public class Mnt_FacilityMap : NopEntityTypeConfiguration<Mnt_Facility>
    {
        public Mnt_FacilityMap()
        {
            this.ToTable("Mnt_Facility");
            this.HasKey(p => p.Id);
        }
    }
}
//Tony Liew 20190417 RDT-202 /\