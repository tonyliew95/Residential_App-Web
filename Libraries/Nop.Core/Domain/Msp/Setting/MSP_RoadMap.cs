using System;
//Tony Liew 20181221 MDT-148 \/
namespace Nop.Core.Domain.Msp.Setting
{
    public partial class MSP_RoadMap : BaseEntity
    {
        public MSP_RoadMap()
        {

        }

        /// <summary>
        /// Get or Set Seq
        /// </summary>
        public byte? Seq { get; set; }

        /// <summary>
        /// Get or Set Title_EN
        /// </summary>
        public string Title_EN { get; set; }

        /// <summary>
        /// Get or Set Title_CN
        /// </summary>
        public string Title_CN { get; set; }

        /// <summary>
        /// Get or Set Content_EN
        /// </summary>
        public string Content_EN { get; set; }

        /// <summary>
        /// Get or Set Content_CN
        /// </summary>
        public string Content_CN { get; set; }

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
    //Tony Liew 20181221 MDT-148 /\
}
