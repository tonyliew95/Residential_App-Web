using Nop.Core.Domain.Residential.Visitor;

namespace Nop.Data.Mapping.Residential.Visitor
{
    public class Trx_Visitor_VehicleMap : NopEntityTypeConfiguration<Trx_Visitor_Vehicle>
    {
        public Trx_Visitor_VehicleMap()
        {
            this.ToTable("Trx_Visitor_Vehicle");
            this.HasKey(p => p.Id);
        }
    }
}
