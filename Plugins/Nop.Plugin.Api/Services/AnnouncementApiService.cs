using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Residential.Announcement;
using Nop.Core.Domain.Residential.Custom;
using Nop.Plugin.Api.Models.Announcement.DTOs;
using Nop.Plugin.Api.Models.Announcement.Request;
using Nop.Plugin.Api.Models.Announcement.ResponseResults;
using Nop.Plugin.Api.Services.Interfaces;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Residential.Announcement;
using Nop.Services.Residential.Helpers.FormatingHelper;
using System.Linq;

namespace Nop.Plugin.Api.Services
{
    /// <summary>
    /// Announcement Api Service Class
    /// </summary>
    public class AnnouncementApiService : IAnnouncementApiService
    {
        private readonly IRepository<Mnt_Announcement> _announcementRepository;
        private readonly IAnnouncementServices _announcementService;
        private readonly ICustomerService _customerService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IFormatingHelper _formatingHelper;

        /// <summary>
        /// Ctor
        /// </summary>
        public AnnouncementApiService
            (
                IRepository<Mnt_Announcement> announcementRepository,
                ICustomerService customerService,
                IEventPublisher eventPublisher,
                IAnnouncementServices announcementService,
                IFormatingHelper formatingHelper
            )
        {
            this._announcementRepository = announcementRepository;
            this._customerService = customerService;
            this._eventPublisher = eventPublisher;
            this._announcementService = announcementService;
            this._formatingHelper = formatingHelper;
        }

        //WKK 20190315 RDT-122 [API] Notice - Announcement detail
        #region Announcement Details

        /// <summary>
        /// Get Announcement Details
        /// </summary>
        /// <param name="announcementId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public AnnouncementDetailsDto GetAnnouncementDetails(int announcementId)
        {
            var result = _announcementService.GetAnnouncementDetails(announcementId).Select(q => new AnnouncementDetailsDto()
            {
                subject = q.subject,
                content = q.content,
                date = _formatingHelper.getDateFormat(q.date),
                media = q.media.AsQueryable()
            })
            .FirstOrDefault();

            return result;
        }

        #endregion

        //WKK 20190315 RDT-121 [API] Notice - Announcement list
        #region Announcement List

        /// <summary>
        /// Get Announcement List
        /// </summary>
        /// <param name="param"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public AnnouncementList_ResponseResult GetAnnouncementList(AnnouncementList_Request param, int CustomerId)
        {
            var responseResult = new AnnouncementList_ResponseResult();

            // return full list if pageNum is -1
            //if (param.pageNum == -1) param.pageSize = 99999;
            //if (param.pageNum < 1) param.pageNum = 1;

            var query = _announcementService.GetAnnouncementList(null, null, CustomerId, param.orgId, param.pageNum, param.pageSize);


            if (param.pageNum >= 1 && param.orgId != -1 && param.pageSize >= 1)
            {
                var result = new PagedList<Mnt_AnnouncementCustom>(query, param.pageNum - 1, param.pageSize);

                responseResult.data.announcementLists = result.ToList().Select(q => new AnnouncementListDto()
                {
                    orgId = q.orgId,
                    id = q.id,
                    subject = q.subject,
                    content = q.content,
                    date = _formatingHelper.getDateFormat(q.date),
                    media = q.media.ToList()
                })
                .ToList();

                responseResult.pagination.pageNum = result.PageIndex + 1;
                responseResult.pagination.pageSize = result.PageSize;
                responseResult.pagination.pageTotal = result.TotalPages;
                responseResult.pagination.totalRecord = result.TotalCount;
            }
            else
            {
                var result = query.ToList().Select(q => new AnnouncementListDto()
                {
                    orgId = q.orgId,
                    id = q.id,
                    subject = q.subject,
                    content = q.content,
                    date = _formatingHelper.getDateFormat(q.date),
                    media = q.media.ToList()
                })
              .ToList();

                responseResult.data.announcementLists = result;
                responseResult.pagination.totalRecord = result.Count;
            }

         

          
            return responseResult;
        }

        #endregion
    }

}
