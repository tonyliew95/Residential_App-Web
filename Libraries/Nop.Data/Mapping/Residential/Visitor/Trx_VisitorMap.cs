using Nop.Core.Domain.Residential.Visitor;

namespace Nop.Data.Mapping.Residential.Visitor
{
    public class Trx_VisitorMap : NopEntityTypeConfiguration<Trx_Visitor>
    {
        public Trx_VisitorMap()
        {
            this.ToTable("Trx_Visitor");
            this.HasKey(p => p.Id);
        }
    }
}
