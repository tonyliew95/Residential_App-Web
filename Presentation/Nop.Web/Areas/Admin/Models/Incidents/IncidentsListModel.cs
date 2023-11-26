using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Web.Areas.Admin.Models.Incidents
{
    public partial class IncidentsListModel : BaseNopModel //wailiang 20190320 RDT-127
    {
        public IncidentsListModel()
        {
            StatusValue = new List<SelectListItem>();
            //Categories = new List<SelectListItem>();
            //Types = new List<SelectListItem>(); //wailiang 20190320 RDT-129
        }

        [NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.List.Title")]
        public string Title { get; set; }

        [NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.List.Inc_DateFrom")]
        [UIHint("DateFrom")]
        public DateTime? Inc_DateFrom { get; set; }

        [NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.List.Inc_DateTo")]
        [UIHint("DateTo")]
        public DateTime? Inc_DateTo { get; set; }

        [NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.List.Status")]
        public string Status { get; set; }
        public IList<SelectListItem> StatusValue { get; set; }

        //[NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.List.Category")]
        //public string Category { get; set; }
        //public IList<SelectListItem> Categories { get; set; }

        //[NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.List.Type")]
        //public string Type { get; set; }
        //public IList<SelectListItem> Types { get; set; } //wailiang 20190320 RDT-129
    }
}