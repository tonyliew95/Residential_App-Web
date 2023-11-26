using Nop.Core.Data;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Facility;
using System.Linq;

//Tony Liew 20190417  \/
namespace Nop.Services.Residential.Facility
{
    public class FacilityServices : IFacilityServices
    {
        private readonly IRepository<Trx_Facility_Booking> _trxFacilityBookingRepository;
        private readonly IRepository<Mnt_Facility> _mntFacilityRepository;

        public FacilityServices
        (
              IRepository<Trx_Facility_Booking> trxFacilityBookingRepository
            , IRepository<Mnt_Facility> mntFacilityRepository
        )
        {
            this._trxFacilityBookingRepository = trxFacilityBookingRepository;
            this._mntFacilityRepository = mntFacilityRepository;
        }
        #region Get Booking Listing
        /// <summary>
        /// Get Booking Listing
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="propId"></param>
        /// <returns></returns>
        public IQueryable<FacilityCustom> BookingListing(int orgId, int propId)//Tony Liew 20190417 RDT-202
        {
            var query = (from trxBooking in _trxFacilityBookingRepository.Table
                         join mntFacility in _mntFacilityRepository.Table
                         on trxBooking.FacilityId equals mntFacility.Id
                         where trxBooking.OrganizationId == orgId && trxBooking.PropertyId == propId
                         select new FacilityCustom()
                         {
                             orgId = trxBooking.OrganizationId,
                             propId = trxBooking.PropertyId,
                             id = trxBooking.Id,
                             name = mntFacility.Name,
                             date = trxBooking.CreatedOnUtc,
                             reqDate = trxBooking.BookingFrom,
                             status = trxBooking.Status,
                             statusName = mntFacility.Status // Temporary solution
                         }).AsQueryable().OrderByDescending(q => q.date);

            return query;
        }
        #endregion
    }
}
//Tony Liew 20190417 /\