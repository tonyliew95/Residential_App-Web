using Nop.Core.Domain.Residential.Organization;

// Tony Liew 20190401 RDT-65 \/
namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_UserProperty_MappingMap : NopEntityTypeConfiguration<Mnt_UserProperty_Mapping>
    {
        public Mnt_UserProperty_MappingMap()
        {
            this.ToTable("Mnt_UserProperty_Mapping");
            this.Ignore(p => p.Id);
            this.HasKey(p => p.OrgUnitPropId); //OrgUnitPropId and UserOrgId are the primary key for the table
            this.HasKey(p => p.UserOrgId);
        }
    }
}
// Tony Liew 20190401 RDT-65 /\
