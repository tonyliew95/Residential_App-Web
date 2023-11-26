using System;

//Tony Liew 20190319 RDT-67 \/
namespace Nop.Core.Domain.Residential.Mobile
{
    public class Mnt_UserMsisdn:BaseEntity
    {
        /// <summary>
        /// Gets or Sets the Customer_Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or Sets the CountryCode
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or Sets the Msisdn
        /// </summary>
        public string Msisdn { get; set; }

        /// <summary>
        /// Gets or Sets the Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or Sets the IsVerified
        /// </summary>
        public bool IsVerified { get; set; }

        /// <summary>
        /// Gets or Sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
//Tony Liew 20190319 RDT-67 /\
