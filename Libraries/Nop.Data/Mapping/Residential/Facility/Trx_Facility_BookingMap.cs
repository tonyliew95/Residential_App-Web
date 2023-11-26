using Nop.Core.Domain.Residential.Facility;

//Tony Liew 20190417 RDT-202 \/
namespace Nop.Data.Mapping.Residential.Facility
{
    public class Trx_Facility_BookingMap : NopEntityTypeConfiguration<Trx_Facility_Booking>
    {
        public Trx_Facility_BookingMap()
        {
            this.ToTable("Trx_Facility_Booking");
            this.HasKey(p => p.Id);
        }
    }
}
//Tony Liew 20190417 RDT-202 /\