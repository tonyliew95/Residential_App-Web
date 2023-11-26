using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Core.Domain.Msp.Security
{
    public partial class MSP_Change_Email_Token : BaseEntity
    {
        public MSP_Change_Email_Token()
        {

        }


        /// <summary>
        /// Gets or sets the Token ID
        /// </summary>
        public Guid TokenID { get; set; }

        /// <summary>
        /// Gets or sets the New Email Address
        /// </summary>
        public string New_Email { get; set; }

        /// <summary>
        /// Gets or sets the Customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the IsValid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the Security Checked
        /// </summary>
        public bool SecurityChecked { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the ExpiredOnUtc
        /// </summary>
        public DateTime? ExpiredOnUtc { get; set; }
    }
}
