using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Base;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.Register.FilterParams
{
    /// <summary>
    /// Sign up with Email (Step 2) input parameters
    /// </summary>
    public class RegisterWithEmailSubmit_FilterParam : FilterParam
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public RegisterWithEmailSubmit_FilterParam()
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
        /// Gets and sets the Transaction Password
        /// </summary>
        //[JsonProperty("TransactionPassword")]
        //public string TransactionPassword { get; set; }

        // WilliamHo 20180922 Hide from swagger(revert for QA 20180925)
        /// <summary>
        /// Gets and sets the Security Questions and Security Answers
        /// </summary>
        //[JsonProperty("Security")]
        //public IList<SecurityQuestionAndAnswer> Security { get; set; }

        /// <summary>
        /// Gets and sets the Public IP Address
        /// </summary>
        //[JsonProperty("Public IP Address")]
        //public IList<string> IPAddress { get; set; }

        /// <summary>
        /// Gets and sets the IsUSCitizen
        /// </summary>
        [JsonProperty("IsUSCitizen")]
        public bool IsUSCitizen { get; set; } //WilliamHo 20180915 MSP-132
    }

}
