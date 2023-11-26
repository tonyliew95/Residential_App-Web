using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Areas.Admin.Models.Announcements
{
    public partial class AnnouncementsListModel : BaseNopModel
    {
        public AnnouncementsListModel()
        {
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.List.CreatedOnFrom")]
        //[UIHint("DateNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateFrom")] //wailiang 20181002 MSP-190
        public DateTime? CreatedOnFrom { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.List.CreatedOnTo")]
        //[UIHint("DateNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTo")] //wailiang 20181002 MSP-190
        public DateTime? CreatedOnTo { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.List.PublishedDateFrom")]
        [UIHint("DateNullable")]
        public DateTime? PublishedDateFrom { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.List.PublishedDateTo")]
        [UIHint("DateNullable")]
        public DateTime? PublishedDateTo { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.List.SearchText")]
        public string SearchText { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.AnnouncementsItems.List.SearchPublished")]
        public int SearchPublishedId { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
    }
}