using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Visitor.DTOs;

// WKK 20190418 RDT-194 [API] Visitor - Favourite Details
namespace Nop.Plugin.Api.Models.Visitor.ResponseResults
{
    /// <summary>
    /// Favourite Details return results
    /// </summary>
    public class FavouriteDetails_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public FavouriteDetails_ResponseResult()
        {
            data = new FavouriteDetails();
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public new FavouriteDetails data { get; set; }

    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class FavouriteDetails
    {
        /// <summary>
        /// Gets or sets the VisitorDto 
        /// </summary>
        [JsonProperty("visitorDto")]
        public VisitorDto visitorDto { get; set; }

    }
}
