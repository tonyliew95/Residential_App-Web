using Nop.Core.Domain.Residential.Visitor;

namespace Nop.Data.Mapping.Residential.Visitor
{
    public class Mnt_VehicleTypeMap : NopEntityTypeConfiguration<Mnt_VehicleType>
    {
        public Mnt_VehicleTypeMap()
        {
            this.ToTable("Mnt_VehicleType");
            this.HasKey(p => p.Id);
        }
    }
}
