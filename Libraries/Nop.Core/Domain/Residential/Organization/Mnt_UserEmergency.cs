using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190411 RDT-177 \/
namespace Nop.Core.Domain.Residential.Organization
{
    public class Mnt_UserEmergency: BaseEntity
    {
        /// <summary>
        /// Gets or Sets CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or Sets EmergencyUserId
        /// </summary>
        public int EmergencyUserId { get; set; }

        /// <summary>
        /// Gets or Sets CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or Sets customerId
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
//Tony Liew 20190411 RDT-177 /\