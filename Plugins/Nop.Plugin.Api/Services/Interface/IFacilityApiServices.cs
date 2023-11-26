using Nop.Plugin.Api.Models.Facility.Request;
using Nop.Plugin.Api.Models.Facility.ResponseResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Services.Interface
{
    public interface IFacilityApiServices
    {
        /// <summary>
        /// Booking List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BookingFacilityListing_ResponseResult BookingListing(BookingFacilityListing_Request request);//Tony Liew 20190417 RDT-202
    }
}
