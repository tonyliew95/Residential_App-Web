using Newtonsoft.Json;


// WKK 20190314 RDT-63 [API] Account - login
namespace Nop.Web.Models
{
    /// <summary>
    /// Api Response
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ApiResponse()
        {
            meta = new Meta();
            data = new object();

            meta.message = "success";//Tony Liew 20190307 RDT-118
            meta.code = 1000;//Tony Liew 20190307 RDT-118
        }

        /// <summary>
        /// Gets or sets the meta
        /// </summary>
        [JsonProperty("meta")]
        public Meta meta { get; set; }

        /// <summary>
        /// Gets or sets the data
        /// </summary>
        [JsonProperty("data")]
        public object data { get; set; }

    }

    /// <summary>
    /// Error code and message object
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty("code")]
        public int code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty("message")]
        public string message { get; set; }
    }
}
