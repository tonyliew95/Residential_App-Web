using Nop.Core.Domain.Residential.Visitor;

namespace Nop.Data.Mapping.Residential.Visitor
{
    public class Mnt_VisitorMap : NopEntityTypeConfiguration<Mnt_Visitor>
    {
        public Mnt_VisitorMap()
        {
            this.ToTable("Mnt_Visitor");
            this.HasKey(p => p.Id);
        }
    }
}
