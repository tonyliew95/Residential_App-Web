using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Facility.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190417 RDT-202 \/
namespace Nop.Plugin.Api.Models.Facility.ResponseResult
{
    /// <summary>
    /// General return results
    /// </summary>
    public class BookingFacilityListing_ResponseResult:ApiResponse , ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public BookingFacilityListing_ResponseResult()
        {
            data = new FacilityDto();
            data.facilityDto = new BookingFacilityListing();
            pagination = new ApiResponsePagination();
        }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(BookingFacilityListingDto);
        }

        /// <summary>
        /// Gets or Sets for the pagination
        /// </summary>
        [JsonProperty("pagination")]
        public ApiResponsePagination pagination { get; set; }

        /// <summary>
        /// Gets or Sets for the pagination
        /// </summary>
        [JsonProperty("data")]
        public new FacilityDto data { get; set; }
    }

    /// <summary>
    /// Store a class for nested data return
    /// </summary>
    public class BookingFacilityListing
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public BookingFacilityListing()
        {
            bookingFacilityList = new List<BookingFacilityListingDto>();
        }
        /// <summary>
        /// Gets or sets the BookingFacilityList 
        /// </summary>
        [JsonProperty("booking")]
        public IList<BookingFacilityListingDto> bookingFacilityList { get; set; }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class FacilityDto
    {
        /// <summary>
        /// Gets or sets the FamilyLists 
        /// </summary>
        [JsonProperty("facilityDto")]
        public BookingFacilityListing facilityDto { get; set; }
    }
}
//Tony Liew 20190417 RDT-202 /\