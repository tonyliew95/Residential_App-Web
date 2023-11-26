using Newtonsoft.Json;
using System;

namespace Nop.Plugin.Api.Models.Register.DTOs
{
    /// <summary>
    /// Sign up with Email results (Step 2)
    /// </summary>
    //[JsonObject(Title = "RegisterWithEmailSubmit")]
    public class RegisterWithEmailSubmitDto
    {
        /// <summary>
        /// Gets or sets the CustomerGUID
        /// </summary>
        [JsonProperty("CustomerGUID")]
        public string CustomerGUID { get; set; }
    }
}
