using Nop.Core.Data;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Incident;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Enumeration;
using Nop.Plugin.Api.Models.Incident.DTOs;
using Nop.Plugin.Api.Models.Incident.Request;
using Nop.Plugin.Api.Models.Incident.ResponseResults;
using Nop.Plugin.Api.Services.Interfaces;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Media;
using Nop.Services.Residential.Helpers.BaseHelper;
using Nop.Services.Residential.Helpers.FormatingHelper;
using Nop.Services.Residential.Incident;
using Nop.Web.Framework.UI.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nop.Services;
using Nop.Plugin.Api.Models.Media;
using Nop.Core;

namespace Nop.Plugin.Api.Services
{
    /// <summary>
    /// Incident Api Service Class
    /// </summary>
    public class IncidentApiService : IIncidentApiService
    {
        private readonly IRepository<Trx_Incident> _incidentRepository;
        private readonly IIncidentServices _incidentService;
        private readonly ICustomerService _customerService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IFormatingHelper _formatingHelper;
        private readonly IBaseHelper _baseHelper;

        /// <summary>
        /// Ctor
        /// </summary>
        public IncidentApiService
        (
            IRepository<Trx_Incident> incidentRepository,
            ICustomerService customerService,
            IEventPublisher eventPublisher,
            IIncidentServices incidentService,
            IFormatingHelper formatingHelper,
            IBaseHelper baseHelper
        )
        {
            this._incidentRepository = incidentRepository;
            this._customerService = customerService;
            this._eventPublisher = eventPublisher;
            this._incidentService = incidentService;
            this._formatingHelper = formatingHelper;
            this._baseHelper = baseHelper;

        }

        #region Incident Report
        /// <summary>
        /// Insert an incident
        /// </summary>
        /// <param name="param">param</param>
        /// <param name="customerId">param</param>
        public void InsertIncidentReport(IncidentReport_Request param , int customerId) //Tony Liew 20190307 RDT-118
        {
            var dateInitiatedIncidentReport = DateTime.UtcNow;// fix the time and date

            string file = RES_FileEnum.incident.ToValue<RES_FileEnum>();

            var newIncident = new Trx_Incident()
            {
                Title = param.incident.incidentTitle,
                Desc = param.incident.incidentDescription,
                Inc_DateTime = _formatingHelper.getDateTimeFormatUTC(_formatingHelper.getDateFormatUTC(param.incident.date), _formatingHelper.getTimeFormatUTC(param.incident.time)),
                Location = param.incident.location,
                CreatedBy = customerId,
                CreatedOnUtc = dateInitiatedIncidentReport,
                AllowShare = true, // Default Value
                Status = IncidentController_Insert.pending.ToValue<IncidentController_Insert>(),
                UpdatedOnUtc = dateInitiatedIncidentReport,
                UpdatedBy = customerId,
                ReportedBy = customerId,
                Organization_Id = param.incident.orgId,
                Property_Id = param.incident.propId 
            };

            _incidentService.InsertIncidentReport(newIncident);

            var newIncidentFile = new List<Trx_Incident_File>();

            for(int i = 0; i < param.incident.media.Count; i++)
            {
                var incidentMedia = new Trx_Incident_File()
                {
                    Inc_Id = newIncident.Id,
                    CreatedOnUtc = DateTime.UtcNow,
                    FileType = param.incident.media.ElementAt(i).type,
                    File_URI = _baseHelper.returnURL(param.incident.media.ElementAt(i).content,  customerId , file , param.incident.media.ElementAt(i).type), 
                    Filename = _formatingHelper.getDateFormatUTC(param.incident.date)
                };
                newIncidentFile.Add(incidentMedia);
            }
            _incidentService.InsertIncidentReportMedia(newIncidentFile);
        }
        #endregion

        #region IncidentDetails
        /// <summary>
        /// Get Incident Details
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IncidentDetails_ResponseResult GetIncidentDetails(IncidentDetails_Request request, int customerId) //Tony Liew 20190306 RDT-117
        {
            var response = new IncidentDetails_ResponseResult();

            var query = _incidentService.GetIncidentDetails(request.id, request.orgId,request.propId, customerId).FirstOrDefault();

            if (query == null)
            {
                response.meta.code = (int)IncidentController_IncidentDetails.invalidIncidentDetails;
                response.meta.message = IncidentController_IncidentDetails.invalidIncidentDetails.ToDescription<IncidentController_IncidentDetails>();
                return response;
            }
            else
            {
                var result = new IncidentDetailsDto()
                {
                    incidentTitle = query.title,
                    description = query.description,
                    location = query.location,
                    date = _formatingHelper.getDateFormat(query.date),
                    time = _formatingHelper.getTimeFormat(query.time),
                    status = query.statusCode,
                    statusName = query.status,
                    propId = query.propId,
                    media = query.media.ToList().Select(r => new MediaCustom()
                    {
                        content = string.Concat(Config.S3ImageUrl, r.content),
                        type = r.type
                    }).ToList()
                };

                response.data.IncidentDetail = result;
                return response;
            }
        }

        #endregion

        /// <summary>
        /// Get Incident by customerId
        /// </summary>
        /// <param name="customerId">id</param>
        public Trx_Incident GetIncidentByCustomerId(int customerId) //Tony Liew 20190301 RDT-4
        {
            var query = (from incident
                         in _incidentRepository.Table
                         where incident.CreatedBy == customerId
                         select incident).FirstOrDefault();
            if (query == null)
                return null;

            return query;
        }

        /// <summary>
        /// Get Incident List
        /// </summary>
        /// <param name="param"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public IncidentList_ResponseResult GetIncidentList(IncidentList_Request param, int CustomerId) //Tony Liew 20190301 RDT-2
        {
            var responseResult = new IncidentList_ResponseResult();

            var query = _incidentService.GetIncidentList(null,null,CustomerId,param.orgId, param.propId);

            if (param.pageNum >= 1 && param.orgId != -1 && param.pageSize != -1)
            {
                var pgList = new PagedList<Trx_IncidentCustom>(query, param.pageNum - 1, param.pageSize);

                var pg_Query = pgList.Select(q => new IncidentListDto()
                {
                    id = q.id,
                    orgId = q.orgId,
                    title = q.title,
                    location = q.location,
                    //date = q.date,
                    reportedDatetime = _formatingHelper.getDateFormat(q.date) + " " + _formatingHelper.getTimeFormat(q.time),
                    //createdDatetime = _formatingHelper.getDateFormat(q.createdDateOnUtc) + " " + _formatingHelper.getTimeFormat(q.createdTimeOnUtc),
                    createdDatetime = _formatingHelper.ToUnixTime(q.createdDateOnUtc),
                    status = q.statusCode,
                    statusName = q.status,
                    propId = q.propId,
                    media = q.media.ToList().Select(p => new mediaDto()
                    {
                        content = string.Concat(Config.S3ImageUrl, p.content),
                        type = p.type
                    }).ToList()
                }).ToList();

                responseResult.data.IncidentLists = pg_Query;
                responseResult.pagination.pageNum = param.pageNum;
                responseResult.pagination.pageSize = param.pageSize;
                responseResult.pagination.pageTotal = pgList.TotalPages;
                responseResult.pagination.totalRecord = pgList.TotalCount;
                return responseResult;

            }
            else
            {
                var resultList = query.ToList().Select(q => new IncidentListDto()
                {
                    id = q.id,
                    orgId = q.orgId,
                    title = q.title,
                    location = q.location,
                    reportedDatetime = _formatingHelper.getDateFormat(q.date) + " " +  _formatingHelper.getTimeFormat(q.time),
                    createdDatetime = _formatingHelper.ToUnixTime(q.createdDateOnUtc),
                    status = q.statusCode,
                    statusName = q.status,
                    propId = q.propId,
                    media = q.media.ToList().Select(p => new mediaDto()
                    {
                        content = string.Concat(Config.S3ImageUrl, p.content),
                        type = p.type
                    }).ToList()
                }).ToList();

                responseResult.data.IncidentLists = resultList;
                responseResult.pagination.totalRecord = resultList.Count;
                return responseResult;

            }
        }
    }
}
