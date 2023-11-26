using FluentValidation.Attributes;
using Nop.Web.Areas.Admin.Validators.UserGuides;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.UserGuides
{
    [Validator(typeof(UserGuidesValidator))]
    public partial class UserGuidesModel : BaseNopEntityModel
    {
        public UserGuidesModel()
        {
        }

        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.Title")]
        public string ContentTitle { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.Content")]
        public string Content { get; set; }

        /// <summary>
        /// Publish date = Start date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.StartDate")]
        //[UIHint("DateTimeNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTime")] //wailiang 20181002 MSP-190
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Expired date = End date
        /// </summary>
        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.EndDate")]
        //[UIHint("DateTimeNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTime")] //wailiang 20181002 MSP-190
        public DateTime? EndDate { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.Published")]
        public bool IsPublished { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.OnlyVisibleToDepositUser")]
        public bool OnlyVisibleToDepositUser { get; set; } //Jerry 20181015 MSP-340

        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.ContentChinese")]
        public string ContentChinese { get; set; } //Tony Liew 20181217 MDT-142

        [NopResourceDisplayName("Admin.ContentManagement.UserGuides.UserGuidesItems.Fields.TitleChinese")]
        public string ContentTittleChinese { get; set; }//Tony Liew 20181217 MDT-142
    }
}