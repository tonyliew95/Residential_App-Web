using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.VideoGuide
{
    public partial class VideoGuideListModel : BaseNopModel
    {
        public VideoGuideListModel()
        {
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.List.CreatedOnFrom")]
        //[UIHint("DateNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateFrom")] //wailiang 20181002 MSP-190
        public DateTime? CreatedOnFrom { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.List.CreatedOnTo")]
        //[UIHint("DateNullable")] //wailiang 20181002 MSP-190
        [UIHint("DateTo")] //wailiang 20181002 MSP-190
        public DateTime? CreatedOnTo { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.List.PublishedDateFrom")]
        [UIHint("DateNullable")]
        public DateTime? PublishedDateFrom { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.List.PublishedDateTo")]
        [UIHint("DateNullable")]
        public DateTime? PublishedDateTo { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.List.SearchText")]
        public string SearchText { get; set; }

        [NopResourceDisplayName("Admin.ContentManagement.Announcements.VideoGuideItems.List.SearchPublished")]
        public int SearchPublishedId { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
    }
}
