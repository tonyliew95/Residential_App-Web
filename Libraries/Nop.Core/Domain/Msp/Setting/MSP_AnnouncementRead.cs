using System;

namespace Nop.Core.Domain.Msp.Setting
{
    public partial class MSP_AnnouncementRead : BaseEntity
    {
        public MSP_AnnouncementRead()
        {
        }

        /// <summary>
        /// Gets or sets the CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the AnnouncementId
        /// </summary>
        public int AnnouncementId { get; set; }

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
