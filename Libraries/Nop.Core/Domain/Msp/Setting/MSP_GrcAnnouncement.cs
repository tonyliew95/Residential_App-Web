using System;
//Tony Liew 20181227 MDT-173 \/
namespace Nop.Core.Domain.Msp.Setting
{
    public partial class MSP_GrcAnnouncement : BaseEntity
    {
        public MSP_GrcAnnouncement()
        {
        }

        /// <summary>
        /// Get or Set Title_EN
        /// </summary>
        public string Title_EN { get; set; }

        /// <summary>
        /// Get or Set ShortDescription_EN
        /// </summary>
        public string ShortDescription_EN { get; set; } //wailiang 20181227 MDT-175

        /// <summary>
        /// Get or Set Content1_EN
        /// </summary>
        public string Content1_EN { get; set; }

        /// <summary>
        /// Get or Set Content2_EN
        /// </summary>
        public string Content2_EN { get; set; }

        /// <summary>
        /// Get or Set Title_CN
        /// </summary>
        public string Title_CN { get; set; }

        /// <summary>
        /// Get or Set ShortDescription_CN
        /// </summary>
        public string ShortDescription_CN { get; set; } //wailiang 20181227 MDT-175

        /// <summary>
        /// Get or Set Content1_CN
        /// </summary>
        public string Content1_CN { get; set; }

        /// <summary>
        /// Get or Set Content2_CN
        /// </summary>
        public string Content2_CN { get; set; }

        /// <summary>
        /// Get or Set PublishedOnUtc
        /// </summary>
        public DateTime? PublishedOnUtc { get; set; }

        /// <summary>
        /// Get or Set ExpiredOnUtc
        /// </summary>
        public DateTime? ExpiredOnUtc { get; set; }

        /// <summary>
        /// Get or Set IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Get or Set CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Get or Set CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Get or Set UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Get or Set UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }
    }
}
//Tony Liew 20181227 MDT-173 /\