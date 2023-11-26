using System;

namespace Nop.Core.Domain.Msp.Setting
{
    public partial class MSP_Announce_Content : BaseEntity
    {
        public MSP_Announce_Content()
        {
        }

        /// <summary>
        /// Gets or sets the ContentType
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the ContentUrl
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// Gets or sets the ContentName
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// Gets or sets the ContentTitle
        /// </summary>
        public string ContentTitle { get; set; }

        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the PublishedOnUtc
        /// </summary>
        public DateTime? PublishedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the ExpiredOnUtc
        /// </summary>
        public DateTime? ExpiredOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the Is Visible To US Citizen
        /// </summary>
        //public bool IsVisibleToUSCitizen { get; set; } //WilliamHo 20181015 MSP-337 //Jerry 20181018 MSP-338
        public bool OnlyVisibleToDepositUser { get; set; } //Jerry 20181018 MSP-338

        /// <summary>
        /// Gets or sets the CreatedOnUtc
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedOnUtc
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the ContentTitle_CN
        /// </summary>
        public string ContentTitle_CN { get; set; } //Tony Liew 20181217 MDT-141

        /// <summary>
        /// Gets or sets the Content_CN
        /// </summary>
        public string Content_CN { get; set; } //Tony Liew 20181217 MDT-141

        /* //WilliamHo 20181227 MDT-185 \/
        /// <summary>
        /// Gets or sets the GRCLanding
        /// </summary>
        public bool GRCLanding { get; set; } //Atiqah 20181218 MDT-144

        /// <summary>
        /// Gets or sets the MSLanding
        /// </summary>
        public bool MSLanding { get; set; } //Atiqah 20181218 MDT-144

        /// <summary>
        /// Gets or sets the MSInformation
        /// </summary>
        public bool MSInformation { get; set; } //Atiqah 20181218 MDT-144

        /// <summary>
        /// Gets or sets the IsGRCPopUp
        /// </summary>
        public bool IsGRCPopUp { get; set; } //Atiqah 20181218 MDT-144

        /// <summary>
        /// Gets or sets the IsMSPopUp
        /// </summary>
        public bool IsMSPopUp { get; set; } //Atiqah 20181218 MDT-144
        //WilliamHo 20181227 MDT-185 /\ */

        #region Chew 20181214 MDT-131 Commented Code
        ///// <summary>
        ///// Gets or sets the IsShutDown
        ///// </summary>
        //public bool IsShutDown { get; set; } //Tony Liew 20181130 MDT-114

        ///// <summary>
        ///// Gets or sets the ShutDownStartOnUtc
        ///// </summary>
        //public DateTime? ShutDownStartOnUtc { get; set; }//Tony Liew 20181130 MDT-114

        ///// <summary>
        ///// Gets or sets ShutDownEndOnUtc
        ///// </summary>
        //public DateTime? ShutDownEndOnUtc { get; set; }//Tony Liew 20181130 MDT-114
        #endregion
    }
}
