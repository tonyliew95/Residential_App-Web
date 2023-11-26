using Nop.Core.Domain.Residential.Mobile;

//Tony Liew 20190319 RDT-67 \/
namespace Nop.Data.Mapping.Residential.Mobile
{
    public class Mnt_UserMsisdnMap : NopEntityTypeConfiguration<Mnt_UserMsisdn>
    {
        public Mnt_UserMsisdnMap()
        {
            this.ToTable("Mnt_UserMsisdn");
            this.HasKey(p => p.Id);
        }
    }
}
//Tony Liew 20190319 RDT-67 /\