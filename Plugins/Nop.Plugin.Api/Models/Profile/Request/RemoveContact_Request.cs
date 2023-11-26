using Newtonsoft.Json;

//Tony Liew 20190415 RDT-200 \/
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// Remove Contact Request
    /// </summary>
    public class RemoveContact_Request 
    {
        /// <summary>
        /// Gets or Sets the contactId
        /// </summary>
        [JsonProperty("contactId")]
        public int contactId { get; set; }
    }
}
//Tony Liew 20190415 RDT-200 /\