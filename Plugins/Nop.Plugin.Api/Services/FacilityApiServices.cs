using Nop.Core;
using Nop.Core.Domain.Residential.Custom;
using Nop.Plugin.Api.Models.Facility.DTOs;
using Nop.Plugin.Api.Models.Facility.Request;
using Nop.Plugin.Api.Models.Facility.ResponseResult;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services.Residential.Facility;
using Nop.Services.Residential.Helpers.FormatingHelper;
using System.Linq;

//Tony Liew 20190417 /\
namespace Nop.Plugin.Api.Services
{
    /// <summary>
    /// Facility Api Service Class
    /// </summary>
    public class FacilityApiServices : IFacilityApiServices
    {
        private readonly IFacilityServices _facilityServices;
        private readonly IFormatingHelper _formatingHelper;

        /// <summary>
        /// Ctor
        /// </summary>
        public FacilityApiServices
        (
            IFacilityServices facilityServices
            , IFormatingHelper formatingHelper
        )
        {
            this._facilityServices = facilityServices;
            this._formatingHelper = formatingHelper;
        }

        #region Booking List
        /// <summary>
        /// Booking List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BookingFacilityListing_ResponseResult BookingListing(BookingFacilityListing_Request request)//Tony Liew 20190417 RDT-202
        {
            var responseResult = new BookingFacilityListing_ResponseResult();
            var query = _facilityServices.BookingListing(request.orgId, request.propId);

            if (!string.IsNullOrEmpty(request.dateFrom))
                query = query.Where(q => q.reqDate >= _formatingHelper.getDateTimeFormatUTC(request.dateFrom, ""));
            if(!string.IsNullOrEmpty(request.dateTo))
                query = query.Where(q => q.reqDate <= _formatingHelper.getDateTimeFormatUTC(request.dateTo, ""));
            if (request.orgId <= 0 || request.pageNum <= 0 || request.pageSize <= 0)
            {
                responseResult.data.facilityDto.bookingFacilityList = query.ToList().Select(q => new BookingFacilityListingDto()
                {
                    orgId = q.orgId,
                    propId = q.propId,
                    record = new Record() { id = q.id, name = q.name, date = q.date, reqDate = q.reqDate, status = q.status, statusName = q.statusName },
                    success = query.Where(p => p.status == "S").ToList().Count.ToString(),
                    pending = query.Where(p => p.status == "P").ToList().Count.ToString(),
                    reschedule = query.Where(p => p.status == "R").ToList().Count.ToString()
                }).ToList();

                responseResult.pagination.totalRecord = responseResult.data.facilityDto.bookingFacilityList.Count;
            }
            else {
                var pgList = new PagedList<FacilityCustom>(query , request.pageNum - 1 , request.pageSize);

                responseResult.data.facilityDto.bookingFacilityList = pgList.ToList().Select(q => new BookingFacilityListingDto()
                {
                    orgId = q.orgId,
                    propId = q.propId,
                    record = new Record() { id = q.id, name = q.name, date = q.date, reqDate = q.reqDate, status = q.status, statusName = q.statusName },
                    success = query.Where(p => p.status == "S").ToList().Count.ToString(),
                    pending = query.Where(p => p.status == "P").ToList().Count.ToString(),
                    reschedule = query.Where(p => p.status == "R").ToList().Count.ToString()
                }).ToList();


                responseResult.pagination.pageNum = request.pageNum;
                responseResult.pagination.pageSize = request.pageSize;
                responseResult.pagination.pageTotal = pgList.TotalPages;
                responseResult.pagination.totalRecord = pgList.TotalCount;

            }

            return responseResult;
        }
        #endregion
    }
}
//Tony Liew 20190417 /\