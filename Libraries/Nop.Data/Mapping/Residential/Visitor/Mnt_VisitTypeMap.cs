using Nop.Core.Domain.Residential.Visitor;

namespace Nop.Data.Mapping.Residential.Visitor
{
    public class Mnt_VisitTypeMap : NopEntityTypeConfiguration<Mnt_VisitType>
    {
        public Mnt_VisitTypeMap()
        {
            this.ToTable("Mnt_VisitType");
            this.HasKey(p => p.Id);
        }
    }
}
