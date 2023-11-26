using FluentValidation.Attributes;
using Nop.Web.Areas.Admin.Validators.VideoGuide;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.VideoGuide
{
    [Validator(typeof(VideoGuideValidator))]
    public partial class VideoGuideModel : BaseNopEntityModel
    {
        public VideoGuideModel()
        {
        }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.Title")]
        public string ContentTitle { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.Content")]
        public string Content { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.ContentName")]
        public string ContentName { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.ContentUrl")]
        public string ContentUrl { get; set; }

        /// <summary>
        /// Publish date = Start date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.StartDate")]
        //[UIHint("DateTimeNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTime")] //wailiang 20181002 MSP-190
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Expired date = End date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.EndDate")]
        //[UIHint("DateTimeNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTime")] //wailiang 20181002 MSP-190
        public DateTime? EndDate { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.Published")]
        public bool IsPublished { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.OnlyVisibleToDepositUser")]
        public bool OnlyVisibleToDepositUser { get; set; } //Jerry 20181015 MSP-341

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.Fields.TitleChinese")]
        public string ContentTittleChinese { get; set; }//Tony Liew 20181217 MDT-143
    }
}
