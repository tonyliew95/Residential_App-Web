using System;
namespace Nop.Core.Domain.Msp.Setting
{
    public partial class MSP_GrcAnnouncementRead : BaseEntity
    {
        public MSP_GrcAnnouncementRead() //wailiang 20181227 MDT-176
        {
        }

        /// <summary>
        /// Get or Set CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Get or Set GrcAnnouncementId
        /// </summary>
        public int GrcAnnouncementId { get; set; }

        /// <summary>
        /// Get or Set CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}