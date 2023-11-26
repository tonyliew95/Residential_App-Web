using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Incident;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.Residential.Incident
{
    public interface IIncidentServices
    {
        /// <summary>
        /// Get Incident List
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="customerId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<Trx_IncidentCustom> GetIncidentList(DateTime? dateFrom, DateTime? dateTo,
            int customerId, int orgId, int propId, int PageIndex = 1, int PageSize = int.MaxValue); //Tony Liew 20190301 RDT-2

        /// <summary>
        /// Get Incident Details
        /// </summary>
        /// <param name="incidentId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IQueryable<Trx_IncidentCustom> GetIncidentDetails(int incidentId, int orgId, int propId, int customerId); //Tony Liew 20190306 RDT-117

        /// <summary>
        /// Insert an incident
        /// </summary>
        /// <param name="incident">incidents</param>
        void InsertIncidentReport(Trx_Incident incidents); //Tony Liew 20190307 RDT-118

        /// <summary>
        /// Insert an incident
        /// </summary>
        /// <param name="incidentMedia">incidents</param>
        void InsertIncidentReportMedia(List<Trx_Incident_File> incidentMedia); //Tony Liew 20190307 RDT-118

        //wailiang 20190320 RDT-127 \/
        IList<SelectListItem> GetIncidentStatusValue();

        IList<SelectListItem> CreateIncidentStatusValue();

        //IList<SelectListItem> GetIncidentCategoryValue();

        //IList<SelectListItem> CreateIncidentCategoryValue();

        //IList<SelectListItem> GetIncidentTypeValue(); //wailiang 20190320 RDT-129

        //IList<SelectListItem> CreateIncidentTypeValue();

        IPagedList<AdminIncidentCustom> GetAllIncident(string _title = null, DateTime? inc_datefrom = null, DateTime? inc_dateto = null, 
            //string inc_category = null, string inc_type = null,
            string _status = null, int pageIndex = 0, int pageSize = int.MaxValue);

        Trx_Incident GetIncidentById(int id);

        void UpdateIncident(Trx_Incident incident);

        void DeleteIncident(Trx_Incident incident);

        //wailiang 20190320 RDT-127 /\

    }
}
