using System;
using System.Collections.Generic;
using System.Linq;

//Tony Liew 20190403 RDT-175 \/
namespace Nop.Core.Domain.Residential.Custom
{
    public class Mnt_FamilyTenantCustom
    {
        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        public int propId { get; set; }

        /// <summary>
        /// Gets or sets the accId 
        /// </summary>
        public int accId { get; set; }

        /// <summary>
        /// Gets or sets the type 
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the accType 
        /// </summary>
        public string accType { get; set; }

        /// <summary>
        /// Gets or sets the countryCode 
        /// </summary>
        public string countryCode { get; set; }

        /// <summary>
        /// Gets or sets the msisdn 
        /// </summary>
        public string msisdn { get; set; }

        /// <summary>
        /// Gets or sets the periodStart 
        /// </summary>
        public DateTime? periodStart { get; set; }

        /// <summary>
        /// Gets or sets the periodEnd 
        /// </summary>
        public DateTime? periodEnd { get; set; }

        /// <summary>
        /// Gets or sets the createdDatetime 
        /// </summary>
        public DateTime createdDatetime { get; set; }

        /// <summary>
        /// Gets or sets the emergency 
        /// </summary>
        public bool emergency { get; set; }

        /// <summary>
        /// Gets or sets the updatedOnUtc 
        /// </summary>
        public DateTime updatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the email 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the info 
        /// </summary>
        public string info { get; set; }

        /// <summary>
        /// Gets or sets the reminder 
        /// </summary>
        public DateTime? reminder { get; set; }

        /// <summary>
        /// Gets or sets the media 
        /// </summary>
        public IQueryable<MediaCustom> media { get; set; }
    }
}
//Tony Liew 20190403 RDT-175 /\