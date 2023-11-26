using Nop.Core.Domain.Residential.Visitor;

namespace Nop.Data.Mapping.Residential.Visitor
{
    public class Trx_Visitor_RecordTimestampMap : NopEntityTypeConfiguration<Trx_Visitor_RecordTimestamp>
    {
        public Trx_Visitor_RecordTimestampMap()
        {
            this.ToTable("Trx_Visitor_RecordTimestamp");
            this.HasKey(p => p.Id);
        }
    }
}
