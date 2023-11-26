using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Mobile;
using Nop.Core.Domain.Residential.Organization;
using Nop.Core.Domain.Residential.User;
using Nop.Core.Domain.Residential.Visitor;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Models.Visitor.DTOs;
using Nop.Plugin.Api.Models.Visitor.Request;
using Nop.Plugin.Api.Models.Visitor.ResponseResults;
using Nop.Plugin.Api.Services.Interfaces;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Residential.Helpers.BaseHelper;
using Nop.Services.Residential.Helpers.FormatingHelper;
using Nop.Services.Residential.Visitor;
using System;
using System.Globalization;
using System.Linq;

namespace Nop.Plugin.Api.Services
{
    /// <summary>
    /// Visitor Api Service Class
    /// </summary>
    public class VisitorApiService : IVisitorApiService
    {
        private readonly IRepository<Mnt_Visitor> _mntVisitorRepository;
        private readonly IRepository<Mnt_Visitor_Favourite> _mntVisitorFavouriteRepository;
        private readonly IRepository<Trx_Visitor> _trxVisitorRepository;
        private readonly IRepository<Trx_Visitor_RecordTimestamp> _trxVisitorRecordTimestampRepository;
        private readonly IRepository<Mnt_UserAccount> _mntUserAccountRepository;
        private readonly IRepository<Mnt_UserProfile> _mntUserProfileRepository;
        private readonly IRepository<Mnt_UserMsisdn> _mntUserMsisdnRepository;
        private readonly IRepository<Mnt_Organization> _mntOrganizationRepository;
        private readonly IRepository<Trx_Visitor_Vehicle> _trxVisitorVehicleRepository;
        private readonly IRepository<Mnt_Visitor_Vehicle> _mntVisitorVehicleRepository;
        private readonly IRepository<Mnt_VisitType> _mntVisitTypeRepository;
        private readonly IRepository<Mnt_OrgUnitProperty> _mntOrgUnitPropertyRepository;
        private readonly IRepository<Mnt_Org_Block> _mntOrgBlockRepository;
        private readonly IRepository<Mnt_Org_Level> _mntOrgLevelRepository;
        private readonly ICustomerService _customerService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IFormatingHelper _formatingHelper;
        private readonly IBaseHelper _baseHelper;
        private readonly IVisitorService _visitorService;

        /// <summary>
        /// Ctor
        /// </summary>
        public VisitorApiService
            (
                IRepository<Mnt_Visitor> mntVisitorRepository,
                IRepository<Trx_Visitor> trxVisitorRepository,
                IRepository<Mnt_Visitor_Favourite> mntVisitorFavouriteRepository,
                IRepository<Trx_Visitor_RecordTimestamp> trxVisitorRecordTimestampRepository,
                IRepository<Mnt_UserAccount> mntUserAccountRepository,
                IRepository<Mnt_UserProfile> mntUserProfileRepository,
                IRepository<Mnt_UserMsisdn> mntUserMsisdnRepository,
                IRepository<Mnt_Organization> mntOrganizationRepository,
                IRepository<Trx_Visitor_Vehicle> trxVisitorVehicleRepository,
                IRepository<Mnt_Visitor_Vehicle> mntVisitorVehicleRepository,
                IRepository<Mnt_VisitType> mntVisitTypeRepository,
                IRepository<Mnt_OrgUnitProperty> mntOrgUnitPropertyRepository,
                IRepository<Mnt_Org_Block> mntOrgBlockRepository,
                IRepository<Mnt_Org_Level> mntOrgLevelRepository,
                ICustomerService customerService,
                IEventPublisher eventPublisher,
                IFormatingHelper formatingHelper,
                IVisitorService visitorService,
                IBaseHelper baseHelper
            )
        {
            this._mntVisitorRepository = mntVisitorRepository;
            this._mntVisitorFavouriteRepository = mntVisitorFavouriteRepository;
            this._trxVisitorRepository = trxVisitorRepository;
            this._trxVisitorRecordTimestampRepository = trxVisitorRecordTimestampRepository;
            this._mntUserAccountRepository = mntUserAccountRepository;
            this._mntUserProfileRepository = mntUserProfileRepository;
            this._mntUserMsisdnRepository = mntUserMsisdnRepository;
            this._mntOrganizationRepository = mntOrganizationRepository;
            this._trxVisitorVehicleRepository = trxVisitorVehicleRepository;
            this._mntVisitorVehicleRepository = mntVisitorVehicleRepository;
            this._mntVisitTypeRepository = mntVisitTypeRepository;
            this._mntOrgUnitPropertyRepository = mntOrgUnitPropertyRepository;
            this._mntOrgBlockRepository = mntOrgBlockRepository;
            this._mntOrgLevelRepository = mntOrgLevelRepository;
            this._customerService = customerService;
            this._eventPublisher = eventPublisher;
            this._formatingHelper = formatingHelper;
            this._baseHelper = baseHelper;
            _visitorService = visitorService;
        }

        //WKK 20190411 RDT-192 [API] Visitor - Record History Details
        #region Get Record History Details

        /// <summary>
        /// Get Record History Details
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public HistoryDetailsDto GetHistoryDetails(int visitId)
        {
            var detail = new HistoryDetailsDto();

            //var visitDetails = (
            //    from trxVisits in _trxVisitorRepository.Table
            //    join visitor in _mntVisitorRepository.Table
            //        on trxVisits.VisitorId equals visitor.Id into joinVisitor
            //    join org in _mntOrganizationRepository.Table
            //        on trxVisits.OrganizationId equals org.Id into joinOrg
            //    join visitType in _mntVisitTypeRepository.Table
            //        on trxVisits.VisitTypeId equals visitType.Id into joinVisitType
            //    from visitor in joinVisitor.DefaultIfEmpty()
            //    from org in joinOrg.DefaultIfEmpty()
            //    from visitType in joinVisitType.DefaultIfEmpty()
            //    where trxVisits.Id == id
            //    select new VisitorDto()
            //    {
            //        id = visitor.Id,
            //        name = visitor.Name,
            //        countryCode = visitor.CountryCode,
            //        msisdn = visitor.Msisdn,
            //        identityNumber = visitor.IcNo,
            //        email = visitor.Email,
            //        purpose = visitType.Code,
            //        visitingDate_ForQuery = trxVisits.VisitingDate,
            //        driveIn = trxVisits.DriveIn,
            //        ResidentId = trxVisits.ResidentId,
            //        ResidentUnit = trxVisits.ResidentUnit,
            //        OrganizationId = trxVisits.OrganizationId,
            //        PropertyId = trxVisits.PropertyId,
            //        OrganizationName = org.Name,
            //        TrxVisitorId = trxVisits.Id,
            //        Image = visitor.Image
            //    })
            //    .FirstOrDefault();

            //visitDetails.visitingDate = _formatingHelper.getDateFormat(visitDetails.visitingDate_ForQuery);

            // get the visitor details
            detail.visitor = GetVisitDetails(visitId);


            //var residentDetails = (
            //    from userProf in _mntUserProfileRepository.Table
            //    join tel in _mntUserMsisdnRepository.Table
            //        on userProf.Msisdn_Id equals tel.Id into joinTel
            //    from tel in joinTel.DefaultIfEmpty()
            //    where userProf.CustomerId == visitDetails.ResidentId
            //    select new ResidentDto()
            //    {
            //        name = userProf.Name,
            //        unit = visitDetails.ResidentUnit,
            //        countryCode = tel.CountryCode,
            //        msisdn = tel.Msisdn
            //    })
            //    .FirstOrDefault();

            // get the resident details
            detail.resident = GetResidentDetails(detail.visitor.ResidentId);
            detail.resident.unit = detail.visitor.ResidentUnit;

            // fill in the property informations
            detail.trxId = visitId;
            detail.orgId = detail.visitor.OrganizationId;
            detail.propId = detail.visitor.PropertyId;
            detail.orgName = detail.visitor.OrganizationName;

            //// clock in clock out details
            //var inout = _trxVisitorRecordTimestampRepository.Table
            //    .Where(io => io.TrxVisitorId == detail.visitor.TrxVisitorId)
            //    .ToList();

            //if (inout.Count > 0)
            //{
            //    // get the time zone offset
            //    var tzOffset = _baseHelper.getUtcOffsetByOrgId(detail.orgId);

            //    var clockIn = inout.Where(i => i.ClockType == "CI").FirstOrDefault();
            //    var clockOut = inout.Where(i => i.ClockType == "CO").FirstOrDefault();

            //    if (clockIn != null)
            //        detail.visitor.clockInTimestamp = _formatingHelper.getTimeFormat(clockIn.TimestampOnUtc.AddHours(tzOffset));

            //    if (clockOut != null)
            //        detail.visitor.clockOutTimestamp = _formatingHelper.getTimeFormat(clockOut.TimestampOnUtc.AddHours(tzOffset));
            //}

            //// get the vehicle details
            //var vehicleDetails = (
            //    from trxVehicle in _trxVisitorVehicleRepository.Table
            //    join mntVehicle in _mntVisitorVehicleRepository.Table
            //        on trxVehicle.VehicleId equals mntVehicle.Id
            //    where trxVehicle.TrxVisitorId == detail.visitor.TrxVisitorId
            //    select new VehicleDto()
            //    {
            //        type = mntVehicle.VehicleType,
            //        number = mntVehicle.V_PlatNumber
            //    })
            //    .FirstOrDefault();

            //detail.visitor.vehicle = vehicleDetails;

            //// try to get the image file type 
            //detail.visitor.media.content = detail.visitor.Image;
            //try
            //{
            //    detail.visitor.media.type = detail.visitor.media.content.Substring(detail.visitor.media.content.LastIndexOf('.') + 1);
            //}
            //catch { };

            return detail;
        }

        /// <summary>
        /// Get Visit Details
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public VisitDto GetVisitDetails(int visitId)
        {
            var visitDetails = (
                from trxVisits in _trxVisitorRepository.Table
                join visitor in _mntVisitorRepository.Table
                    on trxVisits.VisitorId equals visitor.Id into joinVisitor
                join org in _mntOrganizationRepository.Table
                    on trxVisits.OrganizationId equals org.Id into joinOrg
                join visitType in _mntVisitTypeRepository.Table
                    on trxVisits.VisitTypeId equals visitType.Id into joinVisitType
                from visitor in joinVisitor.DefaultIfEmpty()
                from org in joinOrg.DefaultIfEmpty()
                from visitType in joinVisitType.DefaultIfEmpty()
                where trxVisits.Id == visitId
                select new VisitDto()
                {
                    id = visitor.Id,
                    name = visitor.Name,
                    countryCode = visitor.CountryCode,
                    msisdn = visitor.Msisdn,
                    identityNumber = visitor.IcNo,
                    email = visitor.Email,
                    purpose = visitType.Code,
                    visitingDate_ForQuery = trxVisits.VisitingDate,
                    driveIn = trxVisits.DriveIn,
                    ResidentId = trxVisits.ResidentId,
                    ResidentUnit = trxVisits.ResidentUnit,
                    OrganizationId = trxVisits.OrganizationId,
                    PropertyId = trxVisits.PropertyId,
                    OrganizationName = org.Name,
                    TrxVisitorId = trxVisits.Id,
                    Image = visitor.Image
                })
                .FirstOrDefault();

            visitDetails.visitingDate = _formatingHelper.getDateFormat(visitDetails.visitingDate_ForQuery);

            // clock in clock out details
            var inout = _trxVisitorRecordTimestampRepository.Table
                .Where(io => io.TrxVisitorId == visitDetails.TrxVisitorId)
                .ToList();

            if (inout.Count > 0)
            {
                // get the time zone offset
                //var tzOffset = _baseHelper.getUtcOffsetByOrgId(visitDetails.OrganizationId);

                var clockIn = inout.Where(i => i.ClockType == "CI").FirstOrDefault();
                var clockOut = inout.Where(i => i.ClockType == "CO").FirstOrDefault();

                if (clockIn != null)
                    visitDetails.clockInTimestamp = _formatingHelper.ToUnixTime(clockIn.TimestampOnUtc);
                    //visitDetails.clockInTimestamp = _formatingHelper.getTimeFormat(clockIn.TimestampOnUtc.AddHours(tzOffset));

                if (clockOut != null)
                    visitDetails.clockOutTimestamp = _formatingHelper.ToUnixTime(clockOut.TimestampOnUtc);
                    //visitDetails.clockOutTimestamp = _formatingHelper.getTimeFormat(clockOut.TimestampOnUtc.AddHours(tzOffset));
            }

            // get the vehicle details
            var vehicleDetails = (
                from trxVehicle in _trxVisitorVehicleRepository.Table
                join mntVehicle in _mntVisitorVehicleRepository.Table
                    on trxVehicle.VehicleId equals mntVehicle.Id
                where trxVehicle.TrxVisitorId == visitDetails.TrxVisitorId
                select new VehicleDto()
                {
                    type = mntVehicle.VehicleType,
                    number = mntVehicle.V_PlatNumber
                })
                .FirstOrDefault();

            visitDetails.vehicle = vehicleDetails;

            // try to get the image file type 
            visitDetails.media.content = visitDetails.Image;
            try
            {
                visitDetails.media.type =
                    visitDetails.media.content.Substring(visitDetails.media.content.LastIndexOf('.') + 1);
            }
            catch { };

            return visitDetails;
        }

        /// <summary>
        /// Get Resident Details
        /// </summary>
        /// <param name="residentId"></param>
        /// <returns></returns>
        public ResidentDto GetResidentDetails(int residentId)
        {
            var residentDetails = (
                from userProf in _mntUserProfileRepository.Table
                join tel in _mntUserMsisdnRepository.Table
                    on userProf.Msisdn_Id equals tel.Id into joinTel
                from tel in joinTel.DefaultIfEmpty()
                where userProf.CustomerId == residentId
                select new ResidentDto()
                {
                    name = userProf.Name,
                    countryCode = tel.CountryCode,
                    msisdn = tel.Msisdn
                })
                .FirstOrDefault();

            return residentDetails;
        }

        #endregion

        // WKK 20190418 RDT-194 [API] Visitor - Favourite Details
        #region Visitor - Favourite Details

        /// <summary>
        /// Get Favourite Visitor Details
        /// </summary>
        /// <param name="visitorId"></param>
        /// <returns></returns>
        public VisitorDto GetVisitorDetails(int visitorId)
        {
            var visitorDetails = (
                from visitor in _mntVisitorRepository.Table
                join vehicle in _mntVisitorVehicleRepository.Table on
                    visitor.VehicleId equals vehicle.Id into joinVehicle
                join visittype in _mntVisitTypeRepository.Table
                    on visitor.VisitTypeId equals visittype.Id into joinVisittype
                from vehicle in joinVehicle.DefaultIfEmpty()
                from visittype in joinVisittype.DefaultIfEmpty()
                where visitor.Id == visitorId
                select new VisitorDto()
                {
                    id = visitor.Id,
                    name = visitor.Name,
                    countryCode = visitor.CountryCode,
                    msisdn = visitor.Msisdn,
                    identityNumber = visitor.IcNo,
                    email = visitor.Email,
                    driveIn = visitor.DriveIn,
                    Image = visitor.Image,
                    purpose = visittype.Code,
                    vehicleType = vehicle.VehicleType,
                    vehicleNumber = vehicle.V_PlatNumber
                })
                .FirstOrDefault();

            // get the vehicle details
            visitorDetails.vehicle.type = visitorDetails.vehicleType;
            visitorDetails.vehicle.number = visitorDetails.vehicleNumber;
            visitorDetails.media.content = visitorDetails.Image;

            // try to get the image file type 
            try
            {
                visitorDetails.media.type =
                    visitorDetails.media.content.Substring(visitorDetails.media.content.LastIndexOf('.') + 1);
            }
            catch { };

            return visitorDetails;
        }

        #endregion

        //WKK 20190411 RDT-189 [API] Visitor - Record History Listing
        #region Get Record History Listing

        /// <summary>
        /// Get Record History Listing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public HistoryList_ResponseResult GetHistoryListing(HistoryList_Request request, int CustomerId)
        {
            var responseResult = new HistoryList_ResponseResult();

            // return full list if pageNum is -1
            if (request.pageNum == -1) request.pageSize = 99999;
            if (request.pageNum < 1) request.pageNum = 1;

            var query = (
                from trxVisits in _trxVisitorRepository.Table
                //join inout in _trxVisitorRecordTimestampRepository.Table 
                //    on trxVisits.Id equals inout.TrxVisitorId into joinInout
                join visitor in _mntVisitorRepository.Table 
                    on trxVisits.VisitorId equals visitor.Id into joinVisitor
                from visitor in joinVisitor.DefaultIfEmpty()
                //from inout in joinInout.DefaultIfEmpty()
                select new HistoryListDto()
                {
                    orgId = trxVisits.OrganizationId,
                    propId = trxVisits.PropertyId,
                    trxId = trxVisits.Id,
                    name = visitor.Name,
                    //clockType = inout.ClockType,
                    visitingDate_ForQuery = trxVisits.VisitingDate,
                    //clockTimestamp_ForQuery = inout.TimestampOnUtc
                    clock = (from inout in _trxVisitorRecordTimestampRepository.Table
                             where inout.TrxVisitorId == trxVisits.Id
                             select new ClockDto()
                             {
                                 type = inout.ClockType,
                                 clockTimestamp_ForQuery = inout.TimestampOnUtc
                             })
                             .AsQueryable()
                })
                .AsQueryable();

            DateTime? dateFr = null;
            DateTime? dateTo = null;
            if (DateTime.TryParseExact(request.dateFrom, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtFr))
            { dateFr = dtFr; }
            if (DateTime.TryParseExact(request.dateTo, "dd MMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtTo))
            { dateTo = dtTo; }

            if (dateFr.HasValue)
                query = query.Where(q => q.visitingDate_ForQuery >= dateFr);
            if (dateTo.HasValue)
                query = query.Where(q => q.visitingDate_ForQuery <= dateTo);
            if (request.orgId != -1)
                query = query.Where(q => q.orgId == request.orgId);
            if (request.propId != -1)
                query = query.Where(q => q.propId == request.propId);

            query = query.OrderByDescending(q => q.visitingDate_ForQuery);

            var pgList = new PagedList<HistoryListDto>(query, request.pageNum - 1, request.pageSize);

            // format the date
            foreach (var item in pgList)
            {
                if (item.visitingDate_ForQuery.HasValue)
                    item.visitingDate = _formatingHelper.getDateFormat(item.visitingDate_ForQuery);

                // get the time zone offset
                //var tzOffset = _baseHelper.getUtcOffsetByOrgId(item.orgId);

                foreach (var i in item.clock)
                {
                    if (i.clockTimestamp_ForQuery.HasValue)
                    {
                        //i.clockTimestamp_ForQuery = i.clockTimestamp_ForQuery.Value.AddHours(tzOffset);
                        //i.timestamp = _formatingHelper.getTimeFormat(i.clockTimestamp_ForQuery.Value);
                        i.timestamp = _formatingHelper.ToUnixTime(i.clockTimestamp_ForQuery.Value);
                    }

                }

                //if (item.clockTimestamp_ForQuery.HasValue)
                //{
                //    // get the time zone offset
                //    var tzOffset = _baseHelper.getUtcOffsetByOrgId(item.orgId);
                //    item.clockTimestamp_ForQuery = item.clockTimestamp_ForQuery.Value.AddHours(tzOffset);

                //    item.clockTimestamp = _formatingHelper.getTimeFormat(item.clockTimestamp_ForQuery.Value);
                //}
            }

            responseResult.data.visitorDto = pgList;

            responseResult.pagination.pageNum = pgList.PageIndex + 1;
            responseResult.pagination.pageSize = pgList.PageSize;
            responseResult.pagination.pageTotal = pgList.TotalPages;
            responseResult.pagination.totalRecord = pgList.TotalCount;
            return responseResult;
        }

        #endregion

        // WKK 20190415 RDT-190 [API] Visitor - Request Submission
        #region Submit a visitor request.

        /// <summary>
        /// Visitor - Request Submission
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        public int VisitorRequestSubmission(VisitorSubmission_Request request, int customerId)
        {
            // check visitor exist
            if (request.visitorId > 0)
            {
                var visitor = _mntVisitorRepository.Table
                    .Where(p => p.Id == request.visitorId)
                    .FirstOrDefault();

                if (visitor == null)
                    request.visitorId = CreateNewVisitor(request, customerId);
            }
            else
            {
                request.visitorId = CreateNewVisitor(request, customerId);
            }

            // get the ResidentUnit
            var residentUnit = (
                from unit in _mntOrgUnitPropertyRepository.Table
                join block in _mntOrgBlockRepository.Table
                    on unit.BlockId equals block.Id
                join level in _mntOrgLevelRepository.Table
                    on unit.LevelId equals level.Id
                where unit.Id == request.propId
                select new
                {
                    block = block.Name,
                    level = level.Name,
                    unit = unit.UnitNumber
                })
                .FirstOrDefault();

            // create new visit
            var newVisit = new Trx_Visitor()
            {
                OrganizationId = request.orgId,
                PropertyId = request.propId,
                VisitorId = request.visitorId,
                VisitTypeId = _mntVisitTypeRepository.Table.Where(p => p.Code == request.purpose).FirstOrDefault().Id,
                ResidentId = customerId,
                ResidentUnit = residentUnit.block + "-" + residentUnit.level + "-" + residentUnit.unit,
                VisitingDate = Convert.ToDateTime(request.visitingDate),
                DriveIn = request.driveIn.Value,
                Status = TrxVisitorStatus.Approved.ToValue<TrxVisitorStatus>(),
                CreatedBy = customerId,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedBy = customerId,
                UpdatedOnUtc = DateTime.UtcNow
            };

            _trxVisitorRepository.Insert(newVisit);

            // save the vehicle
            if (request.driveIn.Value)
            {
                var mntVehicle = _mntVisitorVehicleRepository.Table
                    .Where(v => v.VisitorId == request.visitorId && v.V_PlatNumber == request.vehicle.number)
                    .FirstOrDefault();

                if (mntVehicle == null)
                {
                    var newVehicle = new Mnt_Visitor_Vehicle()
                    {
                        VisitorId = request.visitorId,
                        VehicleType = request.vehicle.type,
                        V_PlatNumber = request.vehicle.number,
                        Status = true,
                        CreatedBy = customerId,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedBy = customerId,
                        UpdatedOnUtc = DateTime.UtcNow
                    };

                    _mntVisitorVehicleRepository.Insert(newVehicle);
                    mntVehicle = newVehicle;
                }

                var trxVehicle = new Trx_Visitor_Vehicle()
                {
                    TrxVisitorId = newVisit.Id,
                    VehicleId = mntVehicle.Id,
                    OrganizationId = request.orgId,
                    CreatedBy = customerId,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedBy = customerId,
                    UpdatedOnUtc = DateTime.UtcNow
                };

                _trxVisitorVehicleRepository.Insert(trxVehicle);
            }

            return newVisit.Id;
        }

        /// <summary>
        /// Create a new visitor
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns>Mnt_Visitor Id</returns>
        public int CreateNewVisitor(VisitorSubmission_Request request, int customerId)
        {
            // save the media
            string file = RES_FileEnum.visitor.ToValue<RES_FileEnum>();
            string imagePath = _baseHelper.returnURL(request.media.content, customerId, file, request.media.type);

            var newVisitor = new Mnt_Visitor()
            {
                OrganizationId = request.orgId,
                IcNo = request.identityNum,
                Name = request.name,
                CountryCode = request.countryCode,
                Msisdn = request.msisdn,
                Email = request.email,
                Image = imagePath,
                DriveIn = request.driveIn.Value,
                Status = true,
                CreatedBy = customerId,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedBy = customerId,
                UpdatedOnUtc = DateTime.UtcNow
            };

            _mntVisitorRepository.Insert(newVisitor);

            return newVisitor.Id;
        }

        #endregion

        // WKK 20190416 RDT-197 [API] Visitor - Clock In/Out
        #region Clock In/Out

        /// <summary>
        /// Visitor - Clock In/Out
        /// </summary>
        /// <param name="trxId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public void VisitorTimeClock(int trxId, string type)
        {
            if (!_visitorService.CheckDuplicateClockInOut(trxId, type))
            {
                var clockInOut = new Trx_Visitor_RecordTimestamp()
                {
                    TrxVisitorId = trxId,
                    ClockType = type,
                    TimestampOnUtc = DateTime.UtcNow,
                    CreatedBy = 0,
                    CreatedOnUtc = DateTime.UtcNow,
                };

                _trxVisitorRecordTimestampRepository.Insert(clockInOut);
            }
            return;
        }

        #endregion

        //WKK 20190417 RDT-193 [API] Visitor - Favourite Listing
        #region Get Favourite List

        /// <summary>
        /// Get User's visitor favourite list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public FavouriteList_ResponseResult GetFavouriteVisitorList(FavouriteList_Request request, int customerId)
        {
            var responseResult = new FavouriteList_ResponseResult();

            // return full list if pageNum is -1
            if (request.pageNum == -1) request.pageSize = 99999;
            if (request.pageNum < 1) request.pageNum = 1;

            var query = _visitorService.GetFavouriteVisitorList(customerId, request.orgId, request.propId);

            var pgList = new PagedList<FavouriteListCustom>(query, request.pageNum - 1, request.pageSize);

            responseResult.data.visitorDto = pgList;

            // try to get the image file type 
            foreach (var item in responseResult.data.visitorDto)
            {
                try
                {
                    item.media.type = item.media.content.Substring(item.media.content.LastIndexOf('.') + 1);
                }
                catch { };
            }

            responseResult.pagination.pageNum = pgList.PageIndex + 1;
            responseResult.pagination.pageSize = pgList.PageSize;
            responseResult.pagination.pageTotal = pgList.TotalPages;
            responseResult.pagination.totalRecord = pgList.TotalCount;

            return responseResult;
        }

        #endregion

        // WKK 20190417 RDT-195 [API] Visitor - Favourite Add
        /// <summary>
        /// Visitor - Favourite Add
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        public void FavouriteVisitorAdd(FavouriteVisitor_Request request, int customerId)
        {
            // check visitor exist
            if (request.visitorId > 0)
            {
                var visitor = _mntVisitorRepository.Table
                    .Where(p => p.Id == request.visitorId)
                    .FirstOrDefault();

                if (visitor != null)
                {
                    // create new favourite
                    var newFav = new Mnt_Visitor_Favourite()
                    {
                        OrganizationId = request.orgId,
                        PropertyId = request.propId,
                        CustomerId = customerId,
                        VisitorId = request.visitorId,
                        CreatedBy = customerId,
                        CreatedOnUtc = DateTime.UtcNow,
                    };

                    _mntVisitorFavouriteRepository.Insert(newFav);
                }
            }

            return;
        }

        // WKK 20190418 RDT-196 [API] Visitor - Favourite Remove
        /// <summary>
        /// Visitor - Favourite Remove
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId">customer id</param>
        /// <returns></returns>
        public void FavouriteVisitorRemove(FavouriteVisitor_Request request, int customerId)
        {
            // check favourite exist
            if (request.visitorId > 0)
            {
                var favourite = _mntVisitorFavouriteRepository.Table
                    .Where(p => p.VisitorId == request.visitorId
                        && p.OrganizationId == request.orgId
                        && p.PropertyId == request.propId
                        && p.CustomerId == customerId)
                    .FirstOrDefault();

                // delete this favourite
                if (favourite != null)
                    _mntVisitorFavouriteRepository.Delete(favourite);
            }

            return;
        }



    }

}
