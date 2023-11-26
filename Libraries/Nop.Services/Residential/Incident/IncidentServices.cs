using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Incident;
using Nop.Core.Domain.Residential.Organization;
using Nop.Core.Enumeration;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.Residential.Incident
{
    public class IncidentServices:IIncidentServices
    {
        private readonly IRepository<Trx_Incident> _incidentRepository;
        private readonly IRepository<Trx_Incident_File> _incidentFileRepository;
        private readonly IRepository<Mnt_Incident_Category> _incidentCategoryRepository;
        private readonly IRepository<Mnt_Incident_Status> _incidentStatusRepository;
        private readonly IRepository<Mnt_Incident_Type> _incidentTypeRepository;
        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IMspHelper _mspHelper;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Ctor
        /// </summary>
        public IncidentServices
        (
            IRepository<Trx_Incident> incidentRepository,
            IRepository<Trx_Incident_File> incidentFileRepository,
            IRepository<Mnt_Incident_Category> incidentCategoryRepository,
            IRepository<Mnt_Incident_Status> incidentStatusRepository,
            IRepository<Mnt_Incident_Type> incidentTypeRepository,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper,
            IMspHelper mspHelper,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService
            
        )
        {
            this._incidentRepository = incidentRepository;
            this._incidentFileRepository = incidentFileRepository;
            this._incidentCategoryRepository = incidentCategoryRepository;
            this._incidentStatusRepository = incidentStatusRepository;
            this._incidentTypeRepository = incidentTypeRepository;
            this._customerService = customerService;
            this._dateTimeHelper = dateTimeHelper;
            this._mspHelper = mspHelper;
            this._eventPublisher = eventPublisher;
            this._localizationService = localizationService;
        }
        #region Incident Listing
        /// <summary>
        /// Get Incident List
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="customerId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IQueryable<Trx_IncidentCustom> GetIncidentList(DateTime? dateFrom , DateTime? dateTo,
            int customerId, int orgId, int propId, int PageIndex = 1, int PageSize = int.MaxValue) //Tony Liew 20190301 RDT-2
        {

            var query = (from incident
                         in _incidentRepository.Table
                         join status in _incidentStatusRepository.Table
                         on incident.Status equals status.code into statusDesc
                         from incidentName in statusDesc.DefaultIfEmpty()
                         where incident.CreatedBy == customerId && incident.AllowShare && incident.Property_Id == propId
                         select new Trx_IncidentCustom()
                         {
                             id = incident.Id,
                             title = incident.Title,
                             location = incident.Location,
                             date = incident.Inc_DateTime,
                             time = incident.Inc_DateTime,
                             status = incidentName.name,
                             statusCode = incident.Status,
                             createdDateOnUtc = incident.CreatedOnUtc,
                             createdTimeOnUtc = incident.CreatedOnUtc,
                             orgId = incident.Organization_Id,
                             propId = incident.Property_Id,
                             media = (from file in _incidentFileRepository.Table where file.Inc_Id == incident.Id select new MediaCustom() { content = file.File_URI, type = file.FileType }).AsQueryable()
                         }).AsQueryable();

            if (dateFrom.HasValue)
                query = query.Where(q => q.createdDateOnUtc >= dateFrom);
            if (dateTo.HasValue)
                query = query.Where(q => q.createdDateOnUtc <= dateTo);
            if (orgId != -1)
                query = query.Where(q => q.orgId == orgId);

            query = query.OrderByDescending(q => q.createdDateOnUtc);
            return query;
        }
        #endregion

        #region IncidentDetails
        /// <summary>
        /// Get Incident Details
        /// </summary>
        /// <param name="incidentId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IQueryable <Trx_IncidentCustom> GetIncidentDetails(int incidentId, int orgId, int propId, int customerId) //Tony Liew 20190306 RDT-117
        {

            var query = (from incident
                         in _incidentRepository.Table
                         join status in _incidentStatusRepository.Table
                         on incident.Status equals status.code into statusDesc
                         from incidentName in statusDesc.DefaultIfEmpty()
                         where incident.CreatedBy == customerId && incident.Id == incidentId && incident.Organization_Id == orgId && incident.Property_Id == propId
                         select new Trx_IncidentCustom()
                         {
                             title = incident.Title,
                             description = incident.Desc,
                             location = incident.Location,
                             date = incident.Inc_DateTime,
                             time = incident.Inc_DateTime,
                             status = incidentName.name,
                             statusCode = incident.Status,
                             createdDateOnUtc = incident.CreatedOnUtc,
                             createdTimeOnUtc = incident.CreatedOnUtc,
                             propId = incident.Property_Id,
                             media = (from file in _incidentFileRepository.Table where file.Inc_Id == incident.Id select new MediaCustom() { content = file.File_URI, type = file.FileType }).AsQueryable()
                         }).AsQueryable();
            return query;
        }
        #endregion

        #region Incident Report
        /// <summary>
        /// Insert an incident
        /// </summary>
        /// <param name="incident">incidents</param>
        public void InsertIncidentReport(Trx_Incident incidents) //Tony Liew 20190307 RDT-118
        {
            if (incidents == null)
                throw new ArgumentNullException(nameof(incidents));

            _incidentRepository.Insert(incidents);

            //event notification
            _eventPublisher.EntityInserted(incidents);
        }

        /// <summary>
        /// Insert an incident
        /// </summary>
        /// <param name="incident">incidents</param>
        public void InsertIncidentReportMedia(List<Trx_Incident_File> incidentMedia) //Tony Liew 20190307 RDT-118
        {
            if (incidentMedia == null)
                throw new ArgumentNullException(nameof(incidentMedia));

            _incidentFileRepository.Insert(incidentMedia);

            //event notification
           for(int i = 0; i < incidentMedia.Count; i ++)
                _eventPublisher.EntityInserted(incidentMedia.ElementAt(i));
        }
        #endregion

        #region Incident Admin Panel
        //wailiang 20190320 RDT-127 \/
        /// <summary>
        /// GetStatus
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetIncidentStatusValue()
        {
            var _statusValues = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource("Incidents.IncidentsItems.Status.All"), Value = " "},
            };

            var statusAsList = (from data in _incidentStatusRepository.Table
                                select data).ToList();

            if (statusAsList.Count > 0)
            {
                foreach (var status in statusAsList)
                {
                    _statusValues.Add(new SelectListItem { Text = status.name, Value = status.code });
                }
            }
            return _statusValues;
        }

        public IList<SelectListItem> CreateIncidentStatusValue()
        {
            var _statusValues = new List<SelectListItem>();

            var statusAsList = (from data in _incidentStatusRepository.Table
                                select data).ToList();

            if (statusAsList.Count > 0)
            {
                foreach (var status in statusAsList)
                {
                    _statusValues.Add(new SelectListItem { Text = status.name, Value = status.code });
                }
            }
            return _statusValues;
        }
        //wailiang 20190320 RDT-127 /\

        //public IList<SelectListItem> GetIncidentCategoryValue()
        //{
        //    var _categoryValues = new List<SelectListItem>
        //    {
        //        new SelectListItem { Text = _localizationService.GetResource("Incidents.IncidentsItems.Category.All"), Value = " "},
        //    };

        //    var categoryAsList = (from data in _incidentCategoryRepository.Table
        //                          where data.status
        //                          select data).ToList();

        //    if (categoryAsList.Count > 0)
        //    {
        //        foreach (var category in categoryAsList)
        //        {
        //            _categoryValues.Add(new SelectListItem { Text = category.name, Value = category.name });
        //        }
        //    }
        //    return _categoryValues;
        //}

        //public IList<SelectListItem> CreateIncidentCategoryValue()
        //{
        //    var _statusValues = new List<SelectListItem>();

        //    var statusAsList = (from data in _incidentCategoryRepository.Table
        //                        where data.status
        //                        select data).ToList();

        //    if (statusAsList.Count > 0)
        //    {
        //        foreach (var status in statusAsList)
        //        {
        //            _statusValues.Add(new SelectListItem { Text = status.name, Value = status.Id.ToString() });
        //        }
        //    }
        //    return _statusValues;
        //}

        //public IList<SelectListItem> GetIncidentTypeValue() //wailiang 20190320 RDT-129
        //{
        //    var _typeValues = new List<SelectListItem>
        //    {
        //        new SelectListItem { Text = _localizationService.GetResource("Incidents.IncidentsItems.Type.All"), Value = " "},
        //    };

        //    var typeAsList = (from data in _incidentTypeRepository.Table
        //                      where data.status == "A"
        //                      orderby data.priority descending
        //                      select data).ToList();

        //    if (typeAsList.Count > 0)
        //    {
        //        foreach (var type in typeAsList)
        //        {
        //            _typeValues.Add(new SelectListItem { Text = type.name, Value = type.name });
        //        }
        //    }
        //    return _typeValues;
        //}

        //public IList<SelectListItem> CreateIncidentTypeValue()
        //{
        //    var _statusValues = new List<SelectListItem>();

        //    var statusAsList = (from data in _incidentTypeRepository.Table
        //                        where data.status == "A"
        //                        orderby data.priority descending
        //                        select data).ToList();

        //    if (statusAsList.Count > 0)
        //    {
        //        foreach (var status in statusAsList)
        //        {
        //            _statusValues.Add(new SelectListItem { Text = status.name, Value = status.Id.ToString() });
        //        }
        //    }
        //    return _statusValues;
        //}

        public IPagedList<AdminIncidentCustom> GetAllIncident(string _title = null, DateTime? inc_datefrom = null, DateTime? inc_dateto = null, 
            //string inc_category = null, string inc_type = null,
            string _status = null, int pageIndex = 0, int pageSize = int.MaxValue) //wailiang 20190314 RDT-127
        {
            var query = (from data in _incidentRepository.Table
                         join type in _incidentTypeRepository.Table
                         on data.Inc_TypeId equals type.Id into x
                         from typeitem in x.DefaultIfEmpty()
                         join cat in _incidentCategoryRepository.Table
                         on data.Inc_CategoryId equals cat.Id into c
                         from catitem in c.DefaultIfEmpty()
                         select new AdminIncidentCustom
                         {
                             Title = data.Title,
                             Desc = data.Desc,
                             Inc_DateTime = data.Inc_DateTime,
                             Inc_Type = typeitem.name,
                             Inc_Category = catitem.name,
                             Status = data.Status
                         }).AsQueryable();

            if (inc_datefrom.HasValue)
            {
                var fromInc_Date = _dateTimeHelper.ConvertToUtcTime(inc_datefrom.Value);
                query = query.Where(x => x.Inc_DateTime >= fromInc_Date);
            }

            if (inc_dateto.HasValue)
            {
                var toInc_Date = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(inc_dateto.Value));
                query = query.Where(x => x.Inc_DateTime <= toInc_Date);
            }

            if (!string.IsNullOrEmpty(_title))
                query = query.Where(x => x.Title == _title);

            //if (!string.IsNullOrEmpty(inc_category))
            //    query = query.Where(x => x.Inc_Category == inc_category);

            //if (!string.IsNullOrEmpty(inc_type))
            //    query = query.Where(x => x.Inc_Type == inc_type);

            if (!string.IsNullOrEmpty(_status))
                query = query.Where(x => x.Status == _status);

            query = query.OrderByDescending(o => o.Inc_DateTime);

            var Incident = new PagedList<AdminIncidentCustom>(query, pageIndex, pageSize);

            foreach (var item in Incident)
            {
                if (item.Status.Equals("P"))
                    item.Status = "Pending";
                else if (item.Status.Equals("A"))
                    item.Status = "Accepted";
                else if (item.Status.Equals("I"))
                    item.Status = "InProgress";
                else if (item.Status.Equals("C"))
                    item.Status = "Closed";
                else if (item.Status.Equals("X"))
                    item.Status = "Cancel";
                else
                    item.Status = "Success";
            }

            return Incident;
        }

        /// <summary>
        /// Gets an incident by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Trx_Incident GetIncidentById(int id) //wailiang 20190320 RDT-127
        {
            if (id == 0) return null;

            var result = (from x in _incidentRepository.Table
                          where x.Id == id
                          select x).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Updates an incident
        /// </summary>
        /// <param name="incident"></param>
        public virtual void UpdateIncident(Trx_Incident incident) //wailiang 20190320 RDT-127
        {
            if (incident == null)
                throw new ArgumentNullException(nameof(incident));

            _incidentRepository.Update(incident);

            //event notification
            _eventPublisher.EntityUpdated(incident);
        }

        /// <summary>
        /// Deletes an incident
        /// </summary>
        /// <param name="incident"></param>
        public virtual void DeleteIncident(Trx_Incident incident) //wailiang 20190320 RDT-127
        {
            if (incident == null)
                throw new ArgumentNullException(nameof(incident));

            _incidentRepository.Delete(incident);

            //event notification
            _eventPublisher.EntityDeleted(incident);
        }

        #endregion
    }
}
