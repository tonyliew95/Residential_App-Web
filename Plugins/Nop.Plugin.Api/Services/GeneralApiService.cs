using Nop.Core.Data;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Incident;
using Nop.Core.Domain.Residential.Visitor;
using Nop.Plugin.Api.AutoMapper;
using Nop.Plugin.Api.Models.General.DTOs;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services.Events;
using Nop.Services.Residential.Helpers.BaseHelper;
using Nop.Services.Residential.Helpers.FormatingHelper;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Api.Services
{
    /// <summary>
    /// General Api Service Class
    /// </summary>
    public class GeneralApiService : IGeneralApiService
    {
        private readonly IBaseHelper _baseHelper;
        private readonly IEventPublisher _eventPublisher;
        private readonly IFormatingHelper _formatingHelper;
        private readonly IRepository<Mnt_Incident_Status> _mntIncidentStatusRepository;
        private readonly IRepository<Mnt_VehicleType> _mntVehicleTypeRepository;
        private readonly IRepository<Mnt_VisitType> _mntVisitTypeRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        public GeneralApiService
        (
            IBaseHelper baseHelper,
            IEventPublisher eventPublisher,
            IFormatingHelper formatingHelper,
            IRepository<Mnt_Incident_Status> mntIncidentStatusRepository,
            IRepository<Mnt_VehicleType> mntVehicleTypeRepository,
            IRepository<Mnt_VisitType> mntVisitTypeRepository
        )
        {
            this._baseHelper = baseHelper;
            this._eventPublisher = eventPublisher;
            this._formatingHelper = formatingHelper;
            this._mntIncidentStatusRepository = mntIncidentStatusRepository;
            this._mntVehicleTypeRepository = mntVehicleTypeRepository;
            this._mntVisitTypeRepository = mntVisitTypeRepository;

;
        }

        #region Get General Setting
        /// <summary>
        /// Get general setting from Adm_SysControl
        /// </summary>
        /// <returns></returns>
        public SettingDto GetGeneralSetting() //JK 20190322 RDT-166
        {
            SettingDto dto = new SettingDto();
            string domain = _baseHelper.getSettingValueByKey("RES_DomainAddress", "https://res.ggit2u.pw/");

            // Date format
            dto.date = _baseHelper.getSettingValueByKey("RES_DateFormat", "dd MMM yyyy");

            // Time format
            // WilliamHo 20190412, mobile framework only accept "hh:mm aa" format. \/
            //dto.time = _baseHelper.getSettingValueByKey("RES_TimeFormat", "hh:mm tt");
            dto.time = _baseHelper.getSettingValueByKey("RES_TimeFormatMobile", "hh:mm aa");
            // WilliamHo 20190412 /\

            // Page Size
            dto.pageSize = _baseHelper.getSettingValueByKey("RES_DefaultPageSize", "20");

            // Faq Uri
            dto.faq = string.Concat(domain, "faq");

            // Help Support - terms & policy url
            dto.helpSupport = GetHelpSupport(domain);

            // Module - Visible Types
            dto.module.visibleType = _baseHelper.GetStandardCodeListByKey("MODSUB_TYPE");

            // Visitor - Vehicle Types
            dto.visitor.visitor_Vehicle = _baseHelper.GetStandardCodeListByKey("VV_TYPE");

            // Visitor - Purpose Types
            dto.visitor.visitor_Purpose = GetVisitPurpose();

            // Visitor - Timestamp Types
            dto.visitor.visitor_Timestamp = _baseHelper.GetStandardCodeListByKey("VT_TYPE");

            // Incident - Status list
            dto.incident.incident_Status = GetIncidentStatus();

            // Facility - Status list
            dto.facility.facility_Status = _baseHelper.GetStandardCodeListByKey("FAC_ST");

            // FamilyTenant - Family Account Types
            dto.familyTenant.familyAccType = GetFamilyAccType();

            // FamilyTenant - Tenant Account Types
            dto.familyTenant.tenantAccType = GetTenantAccType();

            // Property - Account Types
            dto.property.accType = _baseHelper.GetStandardCodeListByKey("ACCT_TYPE");

            return dto;
        }

        #region GetHelpSupport
        private IList<HelpSupportDto> GetHelpSupport(string domain)
        {
            var list = new List<HelpSupportDto>
            {
                new HelpSupportDto { id = "term", name = "Terms & Conditions", url = string.Concat(domain, "termsandconditions")},
                new HelpSupportDto { id = "policy", name = "Private Policy", url = string.Concat(domain, "privacypolicy")},
            };

            return list;
        }
        #endregion

        #region GetVisitorVehicle
        private IList<Visitor_VehicleDto> GetVisitorVehicle()
        {
            var list = (from type in _mntVehicleTypeRepository.Table
                        select new Visitor_VehicleDto()
                        {
                            code = type.Code,
                            name = type.Name
                        }).ToList();

            return list;
        }
        #endregion

        #region GetVisitPurpose
        private IList<Visitor_PurposeDto> GetVisitPurpose()
        {
            var list = (from purpose in _mntVisitTypeRepository.Table
                        select new Visitor_PurposeDto()
                        {
                            code = purpose.Code,
                            name = purpose.Name
                        }).ToList();

            return list;
        }
        #endregion

        #region GetIncidentStatus
        private IList<Incident_StatusDto> GetIncidentStatus()
        {
            var list = (from status in _mntIncidentStatusRepository.Table
                        select new Incident_StatusDto()
                        {
                            code = status.code,
                            name = status.name
                        }).ToList();

            return list;
        }
        #endregion

        #region GetFamilyAccType
        private IList<Adm_StandardCodeCustom> GetFamilyAccType()
        {
            return new List<Adm_StandardCodeCustom>
            {
                _baseHelper.GetStandardCodeByKeyCode("ACCT_TYPE", "P"),
                _baseHelper.GetStandardCodeByKeyCode("OCPY_TYPE", "F")

            };
        }
        #endregion

        #region GetTenantAccType
        private IList<Adm_StandardCodeCustom> GetTenantAccType()
        {
            return new List<Adm_StandardCodeCustom>
            {
                _baseHelper.GetStandardCodeByKeyCode("ACCT_TYPE", "T"),
                _baseHelper.GetStandardCodeByKeyCode("OCPY_TYPE", "B")

            };
        }
        #endregion
        
        #endregion
    }
}
