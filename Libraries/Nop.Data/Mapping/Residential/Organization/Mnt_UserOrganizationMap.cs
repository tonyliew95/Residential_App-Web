using Nop.Core.Domain.Residential.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_UserOrganizationMap : NopEntityTypeConfiguration<Mnt_UserOrganization>
    {
        public Mnt_UserOrganizationMap()
        {
            this.ToTable("Mnt_UserOrganization");
            this.HasKey(p => p.Id);
        }
    }
}
