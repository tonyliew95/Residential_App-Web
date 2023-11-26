using Nop.Core.Domain.Residential.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_OrgUnitPropertyMap: NopEntityTypeConfiguration<Mnt_OrgUnitProperty>
    {
        public Mnt_OrgUnitPropertyMap()
        {
            this.ToTable("Mnt_OrgUnitProperty");
            this.HasKey(p => p.Id);
        }
    }
}
