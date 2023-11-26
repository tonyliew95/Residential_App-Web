using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Tony Liew 20190417 RDT-202 \/
namespace Nop.Core.Domain.Residential.Custom
{
    public class FacilityCustom 
    {
        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        public int? orgId { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        public int? propId { get; set; }

        /// <summary>
        /// Gets or sets the id 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the date 
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Gets or sets the reqDate 
        /// </summary>
        public DateTime? reqDate { get; set; }

        /// <summary>
        /// Gets or sets the status 
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Gets or sets the statusName 
        /// </summary>
        public string statusName { get; set; }
    }
}
//Tony Liew 20190417 RDT-202 /\