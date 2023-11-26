using FluentValidation.Attributes;
using Nop.Web.Areas.Admin.Validators.Announcements;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Announcements
{
    [Validator(typeof(GrcAnnouncementsValidator))]
    public partial class GrcAnnouncementsModel : BaseNopEntityModel
    {
        public GrcAnnouncementsModel()
        {
        }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.Title_EN")]
        public string Title_EN { get; set; }

		[NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.Title_CN")]
		public string Title_CN { get; set; }

		[NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.Content1_EN")]
        public string Content1_EN { get; set; }
		[NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.Content2_EN")]
		public string Content2_EN { get; set; }

		[NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.Content1_CN")]
		public string Content1_CN { get; set; }
		[NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.Content2_CN")]
		public string Content2_CN { get; set; }

		[NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.ShortDescription_EN")]
		public string ShortDescription_EN { get; set; }
		[NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.ShortDescription_CN")]
		public string ShortDescription_CN { get; set; }

		/// <summary>
		/// Publish date = Start date
		/// </summary>
		[NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.StartDate")]
        [UIHint("DateTime")] 
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Expired date = End date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.EndDate")]
        [UIHint("DateTime")] 
        public DateTime? EndDate { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.Published")]
        public bool IsActive { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}