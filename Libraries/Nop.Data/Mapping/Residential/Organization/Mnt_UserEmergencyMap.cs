using Nop.Core.Domain.Residential.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190411 RDT-177 \/
namespace Nop.Data.Mapping.Residential.Organization
{
    public class Mnt_UserEmergencyMap: NopEntityTypeConfiguration<Mnt_UserEmergency>
    {
        public Mnt_UserEmergencyMap()
        {
            this.ToTable("Mnt_UserEmergency");
            this.HasKey(p => p.Id);
        }
    }
}
//Tony Liew 20190411 RDT-177 /\