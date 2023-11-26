using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.Models.Register.FilterParams
{
    /// <summary>
    /// Sign up with Email (Step 1) input parameters
    /// </summary>
    public class RegisterWithEmail_FilterParam : FilterParam
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public RegisterWithEmail_FilterParam()
        {
        }

        /// <summary>
        /// Gets and sets the Referral Username
        /// </summary>
        [JsonProperty("ReferralUsername")]
        public string ReferralUsername { get; set; }

        /// <summary>
        /// Gets and sets the Username
        /// </summary>
        [JsonProperty("Username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets and sets the Email
        /// </summary>
        [JsonProperty("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets and sets the Password
        /// </summary>
        [JsonProperty("Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets and sets the Confirm Password
        /// </summary>
        //[JsonProperty("ConfirmPassword")]
        //public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets and sets the Transaction Password
        /// </summary>
        //[JsonProperty("TransactionPassword")]
        //public string TransactionPassword { get; set; }

        /// <summary>
        /// Gets and sets the Transaction Confirm Password
        /// </summary>
        //[JsonProperty("TransactionConfirmPassword")]
        //public string TransactionConfirmPassword { get; set; }

        /// <summary>
        /// Gets and sets the IsUSCitizen
        /// </summary>
        [JsonProperty("IsUSCitizen")]
        public bool IsUSCitizen { get; set; }
    }
}
