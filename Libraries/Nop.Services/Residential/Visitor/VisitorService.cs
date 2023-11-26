using Nop.Core.Data;
using Nop.Services.Events;
using System.Linq;
using Nop.Core.Domain.Residential.Visitor;
using Nop.Core.Domain.Residential.Custom;

namespace Nop.Services.Residential.Visitor
{
    public class VisitorService : IVisitorService
    {
        private readonly IRepository<Trx_Visitor_RecordTimestamp> _trxVisitorRecordTimestampRepository;
        private readonly IRepository<Trx_Visitor> _trxVisitorRepository;
        private readonly IRepository<Mnt_Visitor> _mntVisitorRepository;
        private readonly IRepository<Mnt_Visitor_Favourite> _mntVisitorFavouriteRepository;
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Ctor
        /// </summary>
        public VisitorService
            (
                IRepository<Trx_Visitor_RecordTimestamp> trxVisitorRecordTimestampRepository,
                IRepository<Trx_Visitor> trxVisitorRepository,
                IRepository<Mnt_Visitor> mntVisitorRepository,
                IRepository<Mnt_Visitor_Favourite> mntVisitorFavouriteRepository,
                IEventPublisher eventPublisher
            )
        {
            this._trxVisitorRecordTimestampRepository = trxVisitorRecordTimestampRepository;
            this._trxVisitorRepository = trxVisitorRepository;
            this._mntVisitorRepository = mntVisitorRepository;
            this._mntVisitorFavouriteRepository = mntVisitorFavouriteRepository;
            this._eventPublisher = eventPublisher;
        }

        /// <summary>
        /// Check duplicate clock in/out
        /// </summary>
        /// <param name="trxId"></param>
        /// <param name="clockType"></param>
        /// <returns></returns>
        public bool CheckDuplicateClockInOut(int trxId, string clockType)
        {
            var record = _trxVisitorRecordTimestampRepository.Table
                .Where(r => r.TrxVisitorId == trxId && r.ClockType == clockType)
                .FirstOrDefault();

            return (record != null);
        }

        /// <summary>
        /// Get Trx_Visitor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Trx_Visitor GetTrxVisitor(int id)
        {
            var visit = _trxVisitorRepository.Table
                .Where(v => v.Id == id)
                .FirstOrDefault();

            return visit;
        }

        //WKK 20190417 RDT-193 [API] Visitor - Favourite Listing
        /// <summary>
        /// Get User's visitor favourite list
        /// </summary>
        /// <param name="request"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public IQueryable<FavouriteListCustom> GetFavouriteVisitorList(int customerId, int orgId, int propId)
        {
            var query = 
                (
                    from mntVF in _mntVisitorFavouriteRepository.Table
                    join visitor in _mntVisitorRepository.Table
                        on mntVF.VisitorId equals visitor.Id
                    where mntVF.CustomerId == customerId
                    select new FavouriteListCustom()
                    {
                        orgId = mntVF.OrganizationId,
                        propId = mntVF.PropertyId,
                        id = visitor.Id,
                        name = visitor.Name,
                        driveIn = visitor.DriveIn,
                        media = new MediaCustom() { type = "", content = visitor.Image }
                    }
                )
                .AsQueryable();

            if (orgId > 0)
                query = query.Where(q => q.orgId == orgId);
            if (propId > 0)
                query = query.Where(q => q.propId == propId);

            query = query.OrderByDescending(q => q.name);

            return query;

        }

    }
}
