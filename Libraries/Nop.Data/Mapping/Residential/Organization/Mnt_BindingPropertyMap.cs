using Nop.Core.Domain.Residential.Organization;

// Tony Liew 20190401 RDT-65 \/
namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_BindingPropertyMap : NopEntityTypeConfiguration<Mnt_BindingProperty>
    {
        public Mnt_BindingPropertyMap()
        {
            this.ToTable("Mnt_BindingProperty");
            this.Ignore(p => p.Id);
            this.HasKey(p => new { p.UserAccId, p.UserPropId }); //UserAccId and UserPropId are the primary key for the table
        }
    }
}
// Tony Liew 20190401 RDT-65 /\