using FluentValidation.Attributes;
using Nop.Web.Areas.Admin.Validators.Announcements;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Announcements
{
    [Validator(typeof(AnnouncementsValidator))]
    public partial class AnnouncementsModel : BaseNopEntityModel
    {
        public AnnouncementsModel()
        {
        }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.Title")]
        public string ContentTitle { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.Content")]
        public string Content { get; set; }

        /// <summary>
        /// Publish date = Start date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.StartDate")]
        //[UIHint("DateTimeNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTime")] //wailiang 20181002 MSP-190
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Expired date = End date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.EndDate")]
        //[UIHint("DateTimeNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTime")] //wailiang 20181002 MSP-190
        public DateTime? EndDate { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.Published")]
        public bool IsPublished { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.OnlyVisibleToDepositUser")]
        public bool OnlyVisibleToDepositUser { get; set; } //Jerry 20181015 MSP-338

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.ContentChinese")]
        public string ContentChinese { get; set; }//Tony Liew 20181217 MDT-140

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.TitleChinese")]
        public string ContentTittleChinese { get; set; }//Tony Liew 20181217 MDT-140

        /* //WilliamHo 20181227 MDT-185 \/
        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.GRCLanding")]
        public bool IsGRCLanding { get; set; }// Tony Liew 20181218 MDT-161

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.MSLanding")]
        public bool IsMSLanding { get; set; }// Tony Liew 20181218 MDT-161

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.MSInformation")]
        public bool IsMSInformation { get; set; }// Tony Liew 20181218 MDT-161

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.GRCPopup")]
        public bool IsGRCPopUp { get; set; }// Tony Liew 20181218 MDT-161

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.MSPopup")]
        public bool IsMSPopUp { get; set; }// Tony Liew 20181218 MDT-161
        //WilliamHo 20181227 MDT-185 /\ */

        #region Chew 20181214 MDT-131 Commented Code
        //[NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.IsShutDown")]
        //public bool IsShutDownDate { get; set; }//Tony Liew 20181130 MDT-114

        //[NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.ShutDownEndOnUtc")]
        //[UIHint("DateTimeNullable")]
        //public DateTime? ShutDownEndDate { get; set; }//Tony Liew 20181130 MDT-114

        //[NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.Fields.ShutDownStartOnUtc")]
        //[UIHint("DateTimeNullable")]
        //public DateTime? ShutDownStartDate { get; set; }//Tony Liew 20181130 MDT-114
        #endregion
    }
}