using Nop.Core.Domain.Residential.Visitor;

namespace Nop.Data.Mapping.Residential.Visitor
{
    public class Mnt_Visitor_VehicleMap : NopEntityTypeConfiguration<Mnt_Visitor_Vehicle>
    {
        public Mnt_Visitor_VehicleMap()
        {
            this.ToTable("Mnt_Visitor_Vehicle");
            this.HasKey(p => p.Id);
        }
    }
}
