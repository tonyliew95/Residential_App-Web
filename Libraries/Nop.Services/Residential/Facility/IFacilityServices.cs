using Nop.Core.Domain.Residential.Custom;
using System.Linq;

namespace Nop.Services.Residential.Facility
{
    public interface IFacilityServices
    {
        /// <summary>
        /// Get Booking Listing
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="propId"></param>
        /// <returns></returns>
        IQueryable<FacilityCustom> BookingListing(int orgId, int propId);//Tony Liew 20190417 RDT-202
    }
}
