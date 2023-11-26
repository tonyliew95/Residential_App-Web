using FluentValidation.Attributes;
using Nop.Web.Areas.Admin.Validators.Promotions;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Promotions
{
    [Validator(typeof(PromotionsValidator))]
    public partial class PromotionsModel : BaseNopEntityModel
    {
        public PromotionsModel()
        {
        }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.Title")]
        public string ContentTitle { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.Content")]
        public string Content { get; set; }

        /// <summary>
        /// Publish date = Start date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.StartDate")]
        //[UIHint("DateTimeNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTime")] //wailiang 20181002 MSP-190
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Expired date = End date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.EndDate")]
        //[UIHint("DateTimeNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTime")] //wailiang 20181002 MSP-190
        public DateTime? EndDate { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.Published")]
        public bool IsPublished { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.OnlyVisibleToDepositUser")]
        public bool OnlyVisibleToDepositUser { get; set; } //Jerry 20181015 MSP-339

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.ContentChinese")]
        public string ContentChinese { get; set; }//Tony Liew 20181217 MDT-141

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.PromotionsItems.Fields.TitleChinese")]
        public string ContentTittleChinese { get; set; }//Tony Liew 20181217 MDT-141
    }
}