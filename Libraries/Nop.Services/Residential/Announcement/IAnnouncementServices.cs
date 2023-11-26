using Nop.Core;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Announcement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.Residential.Announcement
{
    public interface IAnnouncementServices
    {
        //WKK 20190315 RDT-121 [API] Notice - Announcement list
        /// <summary>
        /// Get Announcement List
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="customerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<Mnt_AnnouncementCustom> GetAnnouncementList(DateTime? dateFrom, DateTime? dateTo,
            int customerId, int orgId, int PageIndex = 1, int PageSize = int.MaxValue); 

        //WKK 20190315 RDT-122 [API] Notice - Announcement detail
        /// <summary>
        /// Get Announcement Details
        /// </summary>
        /// <param name="announcementId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IList<Mnt_AnnouncementCustom> GetAnnouncementDetails(int announcementId); 

    }
}
