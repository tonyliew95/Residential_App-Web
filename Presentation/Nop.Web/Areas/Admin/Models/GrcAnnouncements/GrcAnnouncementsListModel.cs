using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Areas.Admin.Models.Announcements
{
    public partial class GrcAnnouncementsListModel : BaseNopModel
    {
        public GrcAnnouncementsListModel()
        {
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.List.CreatedOnFrom")]
        [UIHint("DateFrom")] 
        public DateTime? CreatedOnFrom { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.List.CreatedOnTo")]
        [UIHint("DateTo")] 
        public DateTime? CreatedOnTo { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.List.PublishedDateFrom")]
        [UIHint("DateNullable")]
        public DateTime? PublishedDateFrom { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.List.PublishedDateTo")]
        [UIHint("DateNullable")]
        public DateTime? PublishedDateTo { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.List.SearchText")]
        public string SearchText { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.GrcAnnouncements.List.SearchPublished")]
        public int SearchActive { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
    }
}