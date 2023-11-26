using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Announcement;
using Nop.Core.Domain.Residential.User;
using Nop.Services.Customers;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.Residential.Organization;
using System.Configuration;

namespace Nop.Services.Residential.Announcement
{
    public class AnnouncementServices : IAnnouncementServices
    {
        private readonly IRepository<Mnt_Announcement> _announcementRepository;
        private readonly ICustomerService _customerService;
        private readonly IRepository<Mnt_Announcement_Attachment> _announcementAttachmentRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Mnt_UserOrganization> _userOrganizationRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        public AnnouncementServices
            (
                IRepository<Mnt_Announcement> announcementRepository,
                ICustomerService customerService,
                IEventPublisher eventPublisher,
                IRepository<Mnt_Announcement_Attachment> announcementAttachmentRepository,
                IRepository<Mnt_UserOrganization> userOrganizationRepository
            )
        {
            this._announcementRepository = announcementRepository;
            this._customerService = customerService;
            this._eventPublisher = eventPublisher;
            this._announcementAttachmentRepository = announcementAttachmentRepository;
            this._userOrganizationRepository = userOrganizationRepository;
    }

        //WKK 20190315 RDT-121 [API] Notice - Announcement list
        #region Announcement Listing

        /// <summary>
        /// Get Announcement List
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="customerId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IQueryable<Mnt_AnnouncementCustom> GetAnnouncementList(DateTime? dateFrom, DateTime? dateTo, 
            int customerId, int orgId, int PageIndex = 1, int PageSize = int.MaxValue)
        {
            String imageServer = ConfigurationManager.AppSettings["S3ImageUrl"];
            
            var query = (
                from announcement in _announcementRepository.Table
                join userOrg in _userOrganizationRepository.Table on announcement.OrganizationId equals userOrg.OrganizationId
                where userOrg.CustomerId == customerId
                select new Mnt_AnnouncementCustom()
                {
                    orgId = announcement.OrganizationId,
                    id = announcement.Id,
                    subject = announcement.Subject,
                    content = announcement.Content,
                    date = announcement.PublishedOnUtc,
                    media = (
                        from file in _announcementAttachmentRepository.Table
                        where file.AnnouncementId == announcement.Id
                        select new MediaCustom()
                        {
                            content = imageServer + file.Path,
                            type = file.MimeType
                        })
                        .AsQueryable()
                })
                .AsQueryable();

            if (dateFrom.HasValue)
                query = query.Select(q => q).Where(q => q.date >= dateFrom);
            if (dateTo.HasValue)
                query = query.Select(q => q).Where(q => q.date <= dateTo);
            if (orgId > 0)
                query = query.Select(q => q).Where(q => q.orgId == orgId);

            query = query.OrderByDescending(q => q.date);

            //var pgList = new PagedList<Mnt_AnnouncementCustom>(query, PageIndex - 1, PageSize);

            return query;
        }

        #endregion

        #region AnnouncementDetails
        //WKK 20190315 RDT-122 [API] Notice - Announcement detail

        /// <summary>
        /// Get Announcement Details
        /// </summary>
        /// <param name="announcementId"></param>
        /// <returns></returns>
        public IList<Mnt_AnnouncementCustom> GetAnnouncementDetails(int announcementId)
        {
            String imageServer = ConfigurationManager.AppSettings["S3ImageUrl"];

            var query = (
                from announcement in _announcementRepository.Table
                where announcement.Id == announcementId
                select new Mnt_AnnouncementCustom()
                {
                    orgId = announcement.OrganizationId,
                    id = announcement.Id,
                    subject = announcement.Subject,
                    content = announcement.Content,
                    date = announcement.PublishedOnUtc,
                    media = (
                        from file in _announcementAttachmentRepository.Table
                        where file.AnnouncementId == announcement.Id
                        select new MediaCustom()
                        {
                            content = imageServer + file.Path,
                            type = file.MimeType
                        })
                        .AsQueryable()
                })
                .ToList();

            return query;
        }

        #endregion

    }
}
