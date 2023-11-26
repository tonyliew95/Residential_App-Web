using Nop.Core.Domain.Residential.Incident;
using Nop.Plugin.Api.Models.Incident.DTOs;
using Nop.Plugin.Api.Models.Incident.Request;
using Nop.Plugin.Api.Models.Incident.ResponseResults;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Services.Interfaces
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IIncidentApiService
    {
        /// <summary>
        /// Insert an incident
        /// </summary>
        /// <param name="param">param</param>
        /// <param name="customerId">param</param>
        void InsertIncidentReport(IncidentReport_Request param, int customerId);//Tony Liew 20190307 RDT-118

        /// <summary>
        /// Get Incident Details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IncidentDetails_ResponseResult GetIncidentDetails(IncidentDetails_Request request, int customerId); //Tony Liew 20190306 RDT-117

        /// <summary>
        /// Get Incident by customerId
        /// </summary>
        /// <param name="customerId">id</param>
        Trx_Incident GetIncidentByCustomerId(int customerId);//Tony Liew 20190301 RDT-4

        /// <summary>
        /// Get Incident List
        /// </summary>
        /// <param name="param"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        IncidentList_ResponseResult GetIncidentList(IncidentList_Request param, int CustomerId); //Tony Liew 20190301 RDT-2
    }
}
