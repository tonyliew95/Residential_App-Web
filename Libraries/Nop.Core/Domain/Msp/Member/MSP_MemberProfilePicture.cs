using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Msp.Member
{
    public partial class MSP_MemberProfilePicture : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public MSP_MemberProfilePicture()
        {
        }

        /// <summary>
        /// Gets or sets the CustomerID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Gets or sets the FileName
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the PictureBinary
        /// </summary>
        public byte[] PictureBinary { get; set; }

        /// <summary>
        /// Gets or sets the MimeType
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUTC
        /// </summary>
        public DateTime CreatedOnUTC { get; set; }

    }
}
