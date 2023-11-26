using Newtonsoft.Json;

//WKK 20190417 RDT-193 [API] Visitor - Favourite Listing
namespace Nop.Core.Domain.Residential.Custom
{
    /// <summary>
    /// FavouriteListCustom class
    /// </summary>
    [JsonObject(Title = "FavouriteListCustom")]
    public class FavouriteListCustom
    {
        /// <summary>
        /// Gets or sets the Id 
        /// </summary>
        [JsonProperty("id")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the driveIn 
        /// </summary>
        [JsonProperty("driveIn")]
        public bool driveIn { get; set; }

        /// <summary>
        /// Gets or sets the media
        /// </summary>
        [JsonProperty("media")]
        public MediaCustom media { get; set; }

        /// <summary>
        /// Gets or sets the orgId
        /// </summary>
        [JsonIgnore]
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the propId
        /// </summary>
        [JsonIgnore]
        [JsonProperty("propId")]
        public int propId { get; set; }


    }
}
