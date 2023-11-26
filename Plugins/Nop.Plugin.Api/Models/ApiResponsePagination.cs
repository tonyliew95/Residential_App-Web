using Newtonsoft.Json;

//Tony Liew 20190306 RDT-117 \/
namespace Nop.Plugin.Api.Models
{
    /// <summary>
    /// Api Response properties for paged list
    /// </summary>
    public class ApiResponsePagination
    {
        /// <summary>
        /// Gets or sets the Pagination Record Count
        /// </summary>
        [JsonProperty("pageTotal")]
        public int pageTotal { get; set; }

        /// <summary>
        /// Gets or sets the Pagination Page Number
        /// </summary>
        [JsonProperty("pageNum")]
        public int pageNum { get; set; }

        /// <summary>
        /// Gets or sets the Pagination Page Size
        /// </summary>
        [JsonProperty("pageSize")]
        public int pageSize { get; set; }

        /// <summary>
        /// Gets or sets the Pagination Total Count
        /// </summary>
        [JsonProperty("totalCount")]
        public int totalRecord { get; set; }
    }
    //Tony Liew 20190306 RDT-117 /\
}
