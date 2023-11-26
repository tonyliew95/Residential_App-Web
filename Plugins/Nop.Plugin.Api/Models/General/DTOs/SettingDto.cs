using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// SettingDto Class
    /// </summary>
    public class SettingDto
    {
        public SettingDto()
        {
            module = new ModuleDto();
            visitor = new VisitorDto();
            incident = new IncidentDto();
            facility = new FacilityDto();
            familyTenant = new FamilyTenantDto();
            property = new PropertyDto();
        }

        /// <summary>
        /// Gets or sets the date format
        /// </summary>
        [JsonProperty("date")]
        public string date { get; set; }

        /// <summary>
        /// Gets or sets the time format 
        /// </summary>
        [JsonProperty("time")]
        public string time { get; set; }

        /// <summary>
        /// Gets or sets the pageSize 
        /// </summary>
        [JsonProperty("pageSize")]
        public string pageSize { get; set; }

        /// <summary>
        /// Gets or sets the faq 
        /// </summary>
        [JsonProperty("faq")]
        public string faq { get; set; }

        /// <summary>
        /// Gets or sets the helpSupport 
        /// </summary>
        [JsonProperty("helpSupport")]
        public IList<HelpSupportDto> helpSupport { get; set; }

        /// <summary>
        /// Gets or sets the module 
        /// </summary>
        [JsonProperty("module")]
        public ModuleDto module { get; set; }

        /// <summary>
        /// Gets or sets the visitor 
        /// </summary>
        [JsonProperty("visitor")]
        public VisitorDto visitor { get; set; }

        /// <summary>
        /// Gets or sets the incident 
        /// </summary>
        [JsonProperty("incident")]
        public IncidentDto incident { get; set; }

        /// <summary>
        /// Gets or sets the facility 
        /// </summary>
        [JsonProperty("facility")]
        public FacilityDto facility { get; set; }

        /// <summary>
        /// Gets or sets the familyTenant 
        /// </summary>
        [JsonProperty("familyTenant")]
        public FamilyTenantDto familyTenant { get; set; }

        /// <summary>
        /// Gets or sets the property 
        /// </summary>
        [JsonProperty("property")]
        public PropertyDto property { get; set; }

    }
}