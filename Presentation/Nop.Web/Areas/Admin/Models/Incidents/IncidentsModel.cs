using FluentValidation.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Validators.Announcements;
using Nop.Web.Areas.Admin.Validators.Promotions;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Incidents
{
    [Validator(typeof(IncidentsValidator))]
    public partial class IncidentsModel : BaseNopEntityModel //wailiang 20190320 RDT-127
    {
        public IncidentsModel()
        {
            StatusValue = new List<SelectListItem>();
            //CategoryValue = new List<SelectListItem>();
            //TypeValue = new List<SelectListItem>(); //wailiang 20190320 RDT-129
        }

        [NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.List.Title")]
        public string Title { get; set; }

        [NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.List.Status")] //same as search because it is same display name
        public string Status { get; set; }
        public IList<SelectListItem> StatusValue { get; set; }

        [NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.Fields.Desc")]
        public string Desc { get; set; }

        [NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.Fields.Inc_Date")]
        [UIHint("DaTeTime")]
        public DateTime Inc_DateTime { get; set; }
        
        //[NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.Fields.Category")]
        //public int Category { get; set; }
        //public IList<SelectListItem> CategoryValue { get; set; }

        //[NopResourceDisplayName("Incidents.IncidentsList.IncidentsItems.Fields.Type")]
        //public int Type { get; set; }
        //public IList<SelectListItem> TypeValue { get; set; } //wailiang 20190320 RDT-129
    }
}